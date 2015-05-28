using Common;
using Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// Maintains and manages a graph of interconnected nodes
    /// More specifically, the nodes' connections
    /// </summary>
    public class NodeGraphManager
    {
        public List<PinConnection> Connections { get; private set; }
        public List<Tuple<GraphNode, int, GraphNode>> ExecutionConnections { get; private set; }
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

        public void RealiseNodeOutputs()
        {
            List<GraphNode> realisedNodes = new List<GraphNode>();
            List<GraphNode> unrealisedNodes = new List<GraphNode>();
            List<GraphNode> realisableNodes = new List<GraphNode>();

            foreach (GraphNode node in nodeNetwork)
            {
                unrealisedNodes.Add(node);
            }

            do
            {
                List<GraphNode> tempList = new List<GraphNode>();

                foreach (GraphNode node in unrealisedNodes)
                {
                    bool isRealisable = true;

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
