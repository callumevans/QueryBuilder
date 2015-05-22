using Common;
using Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class NodeGraphManager
    {
        public List<GraphNode> Nodes { get; set; }
        public List<PinConnection> Connections { get; private set; }
        public QueryState QueryState { get; }

        private List<PinConnection> connections;

        public NodeGraphManager()
        {
            Connections = new List<PinConnection>();
            Nodes = new List<GraphNode>();
            QueryState = new QueryState();
        }

        public void AddConnection(OutputPin outputPin, InputPin targetPin)
        {
            // Make sure we only one connection per input
            foreach (PinConnection connection in this.connections)
            {
                if (connection.InputPin == targetPin) throw new Exception("InputPin already has a connection.");
            }

            connections.Add(new PinConnection(outputPin, targetPin));
        }

        public static IHasExecution NextDownstreamNode(IHasExecution node)
        {
            return node.GetExecutionTarget();
        }

        public static IHasExecution PreviousUpstreamNode(IHasExecution node)
        {
            return node.GetExecutionParent();
        }

        public static List<GraphNode> GetDependentNodes(GraphNode node)
        {
            List<GraphNode> childNodes = new List<GraphNode>();

            foreach (OutputPin outputPin in node.NodeOutputs)
            {
                foreach (InputPin connectedPin in outputPin.Connections)
                {
                    childNodes.Add(connectedPin.Parent);
                }
            }

            return childNodes;
        }
    }
}
