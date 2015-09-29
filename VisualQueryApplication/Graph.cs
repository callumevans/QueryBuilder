using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common;
using DataTypes;
using Graph;
using VisualQueryApplication.ViewModels;
using Boolean = System.Boolean;
using System.Reflection;
using System.Threading;
using System.Windows.Threading;

namespace VisualQueryApplication
{
    public static class Graph
    {
        public async static Task<NodeGraphManager> BuildGraphAsync(GraphEditorViewModel graph)
        {
            return await Task.Run(() => BuildGraph(graph));
        }

        public static NodeGraphManager BuildGraph(GraphEditorViewModel graph)
        {
            List<VisualNodeViewModel> visualExecutionNodes = new List<VisualNodeViewModel>();

            foreach (var graphComponent in graph.VisualNodes)
            {
                VisualNodeViewModel visualNode = graphComponent as VisualNodeViewModel;
                if (visualNode != null && visualNode.ExecutionInputs.Count > 0)
                {
                    visualExecutionNodes.Add(visualNode);
                }
            }

            // Begin to build our graph
            NodeGraphManager graphManager = new NodeGraphManager();

            Dictionary<VisualNodeViewModel, GraphNode> functionNodes = new Dictionary<VisualNodeViewModel, GraphNode>();
            List<VisualConstantNodeViewModel> constantNodes = new List<VisualConstantNodeViewModel>();

            foreach (dynamic visualNode in graph.VisualNodes)
            {
                if (visualNode is VisualNodeViewModel)
                {
                    functionNodes.Add(visualNode, new GraphNode(visualNode.NodeType, graphManager));
                }
                else if (visualNode is VisualConstantNodeViewModel)
                {
                    constantNodes.Add(visualNode as VisualConstantNodeViewModel);
                }
            }

            // Wire up constant nodes
            foreach (var constantNode in constantNodes)
            {
                List<ConnectionViewModel> connections = GetPinConnections(constantNode.OutputPin, graph);

                // Lookup each input pin that the constant node is connecting to and add it in our graph manager
                foreach (var connection in connections)
                {
                    foreach (var functionNode in functionNodes)
                    {
                        if (functionNode.Key.Inputs.Contains(connection.InputPin))
                        {
                            // Get pin index
                            int pindex = ((NodePinViewModel)connection.InputPin).Index;

                            IDataTypeContainer valueToSet;

                            // Get value to set
                            if (constantNode.OutputPin.DataType == typeof(DataTypes.Numeric))
                            {
                                valueToSet = new DataTypes.Numeric(Double.Parse(constantNode.Value));
                            }
                            else if (constantNode.OutputPin.DataType == typeof(DataTypes.Boolean))
                            {
                                valueToSet = new DataTypes.Boolean(Boolean.Parse(constantNode.Value));
                            }
                            else if (constantNode.OutputPin.DataType == typeof (DataTypes.String))
                            {
                                valueToSet = new DataTypes.String(constantNode.Value);
                            } 
                            else
                            {
                                throw new Exception("Data type could not be determined.");
                            }

                            OutputPin pinConnection = new OutputPin(
                                ((NodePinViewModel)connection.InputPin).DataType, null)
                            { OutputValue = valueToSet, OutputRealised = true };


                            graphManager.AddConnection(pinConnection, functionNode.Value.NodeInputs[pindex]);
                        }
                    }
                }
            }

            // Wire up functional nodes
            foreach (var functionNode in functionNodes)
            {
                int outputPindex = 0;

                // Check each output pin on the node
                foreach (var output in functionNode.Key.Outputs)
                {
                    List<ConnectionViewModel> connections = GetPinConnections(output, graph);

                    foreach (var connection in connections)
                    {
                        // Wire up non-execution pins
                        foreach (var targetFunctionNode in functionNodes)
                        {
                            if (targetFunctionNode.Key.Inputs.Contains(connection.InputPin))
                            {
                                int pindex = ((NodePinViewModel)connection.InputPin).Index;

                                graphManager.AddConnection(
                                    functionNodes[functionNode.Key].NodeOutputs[outputPindex],
                                    targetFunctionNode.Value.NodeInputs[pindex]);
                            }
                        }
                    }
                }

                // Wire up execution pins
                foreach (var executionOutput in functionNode.Key.ExecutionOutputs)
                {
                    List<ConnectionViewModel> connections = GetPinConnections(executionOutput, graph);

                    if (connections.Count == 0)
                    {
                        graphManager.AddNode(functionNode.Value);
                    }

                    foreach (var connection in connections)
                    {
                        foreach (var targetFunctionNode in functionNodes)
                        {
                            if (targetFunctionNode.Key.ExecutionInputs.Contains(connection.InputPin))
                            {
                                int pindex = ((NodePinViewModel)connection.OutputPin).Index;
                                graphManager.AddConnection(functionNode.Value, pindex, targetFunctionNode.Value);
                            }
                        }
                    }
                }
            }

            graphManager.RealiseNodeOutputs();

            return graphManager;
        }

        public static List<ConnectionViewModel> GetPinConnections(NodePinViewModel pin, GraphEditorViewModel graph)
        {
            List<ConnectionViewModel> pinConnections = new List<ConnectionViewModel>();

            foreach (var connection in graph.Connections)
            {
                NodePinViewModel inputPin = connection.InputPin;
                NodePinViewModel outputPin = connection.OutputPin;

                if (pin == inputPin || pin == outputPin)
                {
                    pinConnections.Add(connection);
                }
            }

            return pinConnections;
        }

        /// <summary>
        /// Builds an HTML or SQL query from the query data taken from predefined testing nodes.
        /// Testing purposes only.
        /// </summary>
        /// <param name="state">State object to build from.</param>
        /// <returns></returns>
        public static string BuildQuery(QueryState state)
        {
            string outString = "";

            // Process as a print statement
            if (state.VariableBag.ContainsKey("DebugPrint"))
            {
                object debugString;
                state.VariableBag.TryGetValue("DebugPrint", out debugString);

                outString += debugString.ToString();
            }
            // Process as an SQL statement
            else if (state.VariableBag.ContainsKey("SelectFields"))
            {
                // Select
                if (state.VariableBag.ContainsKey("SelectFields"))
                {
                    object fields;
                    state.VariableBag.TryGetValue("SelectFields", out fields);

                    List<string> selectFields = (List<string>) fields;

                    outString = "SELECT ";

                    for (int i = 0; i < selectFields.Count; i++)
                    {
                        // Don't add comma
                        if (i == selectFields.Count - 1)
                            outString += selectFields[i];
                        else
                            outString += selectFields[i] + ",";
                    }
                }

                // From
                if (state.VariableBag.ContainsKey("FromTable"))
                {
                    outString += " ";
                    outString += "FROM " + state.VariableBag["FromTable"];
                }

                // Where
                if (state.VariableBag.ContainsKey("WhereFields"))
                {
                    outString += " ";
                    outString += "WHERE ";

                    object whereList;
                    state.VariableBag.TryGetValue("WhereFields", out whereList);

                    List<Tuple<string, string, string>> castList = (List<Tuple<string, string, string>>) whereList;

                    foreach (var whereCondition in castList)
                    {
                        outString += whereCondition.Item1 + " " + whereCondition.Item2 + "\"" + whereCondition.Item3 + "\"";
                    }
                }

                outString += ";";
            }
            // Process as an HTML statement
            else if (state.VariableBag.ContainsKey("HTML"))
            {
                outString += state.VariableBag["HTML"].ToString();
            }

            return outString;
        }
    }
}