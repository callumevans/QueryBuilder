using Common;
using System;
using System.Collections.Generic;

namespace Graph
{
    /// <summary>
    /// Maintains and manages a graph of interconnected nodes
    /// More specifically, the nodes' connections
    /// </summary>
    public class NodeGraphManager
    {
        /// <summary>
        /// Connections between input and output pins
        /// </summary>
        public List<PinConnection> Connections { get; private set; }

        /// <summary>
        /// Execution paths between executable nodes
        /// </summary>
        public List<Tuple<GraphNode, int, GraphNode>> ExecutionConnections { get; private set; }

        /// <summary>
        /// The query state of the graph being built
        /// </summary>
        public QueryState QueryState { get; }

        private List<GraphNode> nodeNetwork;

        public NodeGraphManager()
        {
            Connections = new List<PinConnection>();
            ExecutionConnections = new List<Tuple<GraphNode, int, GraphNode>>();
            QueryState = new QueryState();

            nodeNetwork = new List<GraphNode>();
        }

        /// <summary>
        /// Add a new connection between two pins
        /// </summary>
        /// <param name="outputPin">The output pin that will provide a value</param>
        /// <param name="targetPin">The input pin the will receive the input</param>
        public void AddConnection(OutputPin outputPin, InputPin targetPin)
        {
            // Make sure we only have one connection per input
            foreach (PinConnection connection in Connections)
            {
                if (connection.InputPin == targetPin) throw new Exception("InputPin already has a connection.");
            }

            Connections.Add(new PinConnection(outputPin, targetPin));

            // Add the connected nodes to our model if we need to
            if ((nodeNetwork.Contains(outputPin.Parent) == false) && (outputPin.Parent != null))
                nodeNetwork.Add(outputPin.Parent);

            if (nodeNetwork.Contains(targetPin.Parent) == false)
                nodeNetwork.Add(targetPin.Parent);
        }

        /// <summary>
        /// Adds a new execution connection between two nodes
        /// </summary>
        /// <param name="rootNode">Node to make connection from</param>
        /// <param name="connectionNumber">Execution index being set on root node</param>
        /// <param name="targetNode">The target node for the execution path</param>
        public void AddConnection(GraphNode rootNode, int connectionNumber, GraphNode targetNode)
        {
            ExecutableNode root = (ExecutableNode)Activator.CreateInstance(rootNode.NodeType, new object[] { QueryState });
            ExecutableNode target = (ExecutableNode)Activator.CreateInstance(targetNode.NodeType, new object[] { QueryState });

            ExecutionConnections.Add(new Tuple<GraphNode, int, GraphNode>(rootNode, connectionNumber, targetNode));

            // Add the connected nodes to our model if we need to
            if (nodeNetwork.Contains(rootNode) == false)
                nodeNetwork.Add(rootNode);

            if (nodeNetwork.Contains(targetNode) == false)
                nodeNetwork.Add(targetNode);
        }

        /// <summary>
        /// Realises all node outputs
        /// </summary>
        public void RealiseNodeOutputs()
        {
            List<GraphNode> unrealisedNodes = new List<GraphNode>();
            List<GraphNode> realisableNodes = new List<GraphNode>();

            GraphNode currentExecutionNode = null;

            List<GraphNode> executableNodes = new List<GraphNode>();

            // Reference executable nodes
            foreach (Tuple<GraphNode, int, GraphNode> connection in ExecutionConnections)
            {
                if (!executableNodes.Contains(connection.Item1))
                    executableNodes.Add(connection.Item1);

                if (!executableNodes.Contains(connection.Item3))
                    executableNodes.Add(connection.Item3);
            }

            // Get start node and set it to the currentExecutionNode
            foreach (var executableNode in executableNodes)
            {
                int connectionsIn = 0;

                foreach (var executionConnection in ExecutionConnections)
                {
                    if (executionConnection.Item3 == executableNode)
                        connectionsIn++;
                }

                if (connectionsIn > 0)
                    continue;
                else if (connectionsIn == 0)
                    currentExecutionNode = executableNode;
            }

            // Assume all nodes are unrealised
            foreach (GraphNode node in nodeNetwork)
            {
                unrealisedNodes.Add(node);
            }

            // Until we have no unrealised nodes
            do
            {
                List<GraphNode> tempList = new List<GraphNode>();

                // See if any nodes are realisable
                foreach (GraphNode node in unrealisedNodes)
                {
                    bool isRealisable = true;

                    // If this is an execution node
                    if (executableNodes.Contains(node) && node != currentExecutionNode)
                    {
                        foreach (var connection in ExecutionConnections)
                        {
                            if (connection.Item1 == currentExecutionNode && connection.Item3 == node)
                            {
                                if (currentExecutionNode.ExecutionPath == connection.Item2)
                                    currentExecutionNode = node;
                            }
                        }

                        // Then make sure it is executable
                        if (node != currentExecutionNode)
                        {
                            continue;
                        }
                    }

                    foreach (InputPin inPin in node.NodeInputs)
                    {
                        if (inPin.InputProviderPin.OutputRealised == false)
                            isRealisable = false;
                    }

                    if (isRealisable)
                    {
                        realisableNodes.Add(node);
                        tempList.Add(node);
                    }
                }

                // If we don't have any realisable nodes then assume graph execution is complete
                if (realisableNodes.Count == 0)
                {
                    break;
                }

                // Calculate outputs for realisable nodes
                foreach (GraphNode realisable in realisableNodes)
                {
                    realisable.CalculateOutput();
                }

                // Cleanup
                foreach (GraphNode cleanupNode in tempList)
                {
                    realisableNodes.Remove(cleanupNode);
                    unrealisedNodes.Remove(cleanupNode);
                }

            } while (unrealisedNodes.Count > 0);
        }
    }
}
