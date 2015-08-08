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

namespace VisualQueryApplication.Controls.GraphBuilder
{
    public static class Graph
    {
        public static NodeGraphManager BuildGraph(GraphEditorViewModel graph)
        {
            // Find the start node
            VisualNodeViewModel startNode;

            List<VisualNodeViewModel> visualExecutionNodes = new List<VisualNodeViewModel>();

            foreach (var graphComponent in graph.VisualNodes)
            {
                VisualNodeViewModel visualNode = graphComponent as VisualNodeViewModel;
                if (visualNode != null && visualNode.ExecutionInputs.Count > 0)
                {
                    visualExecutionNodes.Add(visualNode);
                }
            }

            // Check each executable node's input pin
            foreach (var node in visualExecutionNodes)
            {
                // If the input pin has no connections then we can use it as our start point
                if (GetPinConnections(
                    node.ExecutionInputs[0].Pin.DataContext as NodePinViewModel, graph).Count == 0)
                {
                    startNode = node;
                    break;
                }

                // TODO: Make sure only one node has an execution input with no connections
            }

            // Begin to build our graph
            NodeGraphManager graphManager = new NodeGraphManager();

            Dictionary<VisualNodeViewModel, GraphNode> functionNodes = new Dictionary<VisualNodeViewModel, GraphNode>();
            List<VisualConstantNodeViewModel> constantNodes = new List<VisualConstantNodeViewModel>();

            foreach (dynamic visualNode in graph.VisualNodes)
            {
                if (visualNode is VisualNodeViewModel)
                {
                    // TODO: Graph nodes should be null and created later on
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
                        if (functionNode.Key.Inputs.Contains(connection.InputPin.DataContext))
                        {
                            // Get pin index
                            int pindex = ((NodePinViewModel)connection.InputPin.DataContext).Index;

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
                                ((NodePinViewModel)connection.InputPin.DataContext).DataType, null)
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
                            if (targetFunctionNode.Key.Inputs.Contains(connection.InputPin.DataContext))
                            {
                                int pindex = ((NodePinViewModel)connection.InputPin.DataContext).Index;

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
                            if (targetFunctionNode.Key.ExecutionInputs.Contains(connection.InputPin.DataContext))
                            {
                                int pindex = ((NodePinViewModel)connection.OutputPin.DataContext).Index;
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
                NodePinViewModel inputPin = (NodePinViewModel)connection.InputPin.DataContext;
                NodePinViewModel outputPin = (NodePinViewModel)connection.OutputPin.DataContext;

                if (pin == inputPin || pin == outputPin)
                {
                    pinConnections.Add(connection);
                }
            }

            return pinConnections;
        }
    }
}
