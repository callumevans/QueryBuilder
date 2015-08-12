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
                            if (constantNode.OutputPin.DataType == typeof(DataTypes.Integer))
                            {
                                valueToSet = new DataTypes.Integer(Int32.Parse(constantNode.Value));
                            }
                            else if (constantNode.OutputPin.DataType == typeof(DataTypes.Boolean))
                            {
                                valueToSet = new DataTypes.Boolean(Boolean.Parse(constantNode.Value));
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

        public static string BuildQuery(QueryState state)
        {
            foreach (var variable in state.VariableBag)
            {
                if (variable.Key == "Print Function")
                    return variable.Value.ToString();
            }

            return "NULL";
        }
    }
}
