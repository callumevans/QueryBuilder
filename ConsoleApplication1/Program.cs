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

            // First node
            GraphNode addNode = new GraphNode(typeof(AddNode), manager);

            manager.AddConnection(new OutputPin(typeof(DEBUGINTEGER), null) { OutputValue = new DEBUGINTEGER(3) }, addNode.NodeInputs[0]);
            manager.AddConnection(new OutputPin(typeof(DEBUGINTEGER), null) { OutputValue = new DEBUGINTEGER(6) }, addNode.NodeInputs[1]);

            addNode.CalculateOutput();

            // Second node
            GraphNode addNode2 = new GraphNode(typeof(AddNode), manager);

            manager.AddConnection(addNode.NodeOutputs[0], addNode2.NodeInputs[0]);
            manager.AddConnection(new OutputPin(typeof(DEBUGINTEGER), null) { OutputValue = new DEBUGINTEGER(1) }, addNode2.NodeInputs[1]);

            addNode2.CalculateOutput();

            // Third node
            GraphNode subtractNode = new GraphNode(typeof(SubtractNode), manager);

            manager.AddConnection(addNode2.NodeOutputs[0], subtractNode.NodeInputs[0]);
            manager.AddConnection(new OutputPin(typeof(DEBUGINTEGER), null) { OutputValue = new DEBUGINTEGER(2) }, subtractNode.NodeInputs[1]);

            subtractNode.CalculateOutput();

            Console.WriteLine(subtractNode.NodeOutputs[0].OutputValue.GetDataAsString());

            Console.ReadLine();
        }
    }
}
