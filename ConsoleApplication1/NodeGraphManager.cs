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

        public NodeGraphManager()
        {
            Connections = new List<PinConnection>();
            ExecutionConnections = new List<Tuple<GraphNode, int, GraphNode>>();
            QueryState = new QueryState();
        }

        public void GenerateAllNodeOutputs()
        {

        }

        /// <summary>
        /// Add a new connection between two pins
        /// </summary>
        /// <param name="outputPin">The output pin that will provide a value</param>
        /// <param name="targetPin">The input pin the will receive the input</param>
        public void AddConnection(OutputPin outputPin, InputPin targetPin)
        {
            // Make sure we only one connection per input
            foreach (PinConnection connection in Connections)
            {
                if (connection.InputPin == targetPin) throw new Exception("InputPin already has a connection.");
            }

            Connections.Add(new PinConnection(outputPin, targetPin));
        }

        /// <summary>
        /// Adds a new execution connection between two nodes
        /// </summary>
        /// <param name="rootNode">Node to make connection from</param>
        /// <param name="connectionNumber">Execution index being set on root node</param>
        /// <param name="targetNode">The target node for the execution path</param>
        public void AddConnection(GraphNode rootNode, int connectionNumber, GraphNode targetNode)
        {
            if ((rootNode.NodeType == typeof(IHasExecution)) && (targetNode.NodeType == typeof(IHasExecution)))
                ExecutionConnections.Add(new Tuple<GraphNode, int, GraphNode>(rootNode, connectionNumber, targetNode));
        }

        private List<GraphNode> GetDependencies(GraphNode node)
        {

        }
    }
}
