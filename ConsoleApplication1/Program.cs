using Common;
using DataTypes;
using Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            NodeGraphManager manager = new NodeGraphManager();

            // End node
            GraphNode printNode = new GraphNode(typeof(PrintNode), manager);

            // Start node
            GraphNode startNode = new GraphNode(typeof(StartNode), manager);

            manager.AddConnection(startNode, 1, printNode);

            // First node
            GraphNode addNode = new GraphNode(typeof(AddNode), manager);

            manager.AddConnection(new OutputPin(typeof(DEBUGINTEGER), null) { OutputValue = new DEBUGINTEGER(3), OutputRealised = true }, addNode.NodeInputs[0]);
            manager.AddConnection(new OutputPin(typeof(DEBUGINTEGER), null) { OutputValue = new DEBUGINTEGER(6), OutputRealised = true }, addNode.NodeInputs[1]);

            // Second node
            GraphNode addNode2 = new GraphNode(typeof(AddNode), manager);

            manager.AddConnection(addNode.NodeOutputs[0], addNode2.NodeInputs[0]);
            manager.AddConnection(new OutputPin(typeof(DEBUGINTEGER), null) { OutputValue = new DEBUGINTEGER(1), OutputRealised = true }, addNode2.NodeInputs[1]);

            // Third node
            GraphNode subtractNode = new GraphNode(typeof(SubtractNode), manager);

            manager.AddConnection(addNode2.NodeOutputs[0], subtractNode.NodeInputs[0]);
            manager.AddConnection(new OutputPin(typeof(DEBUGINTEGER), null) { OutputValue = new DEBUGINTEGER(2), OutputRealised = true }, subtractNode.NodeInputs[1]);

            manager.AddConnection(subtractNode.NodeOutputs[0], printNode.NodeInputs[0]);

            //

            manager.RealiseNodeOutputs();

            Console.WriteLine("Waiting");

            Console.ReadLine();
        }

        private static void Manager_muhEvent(string s)
        {
            Console.WriteLine("Progress report...");
        }
    }
}
