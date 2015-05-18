using Contracts;
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

        public NodeGraphManager()
        {

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
