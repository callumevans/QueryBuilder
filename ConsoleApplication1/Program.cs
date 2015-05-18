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
            // First node
            GraphNode addNode = new GraphNode(typeof(AddNode));

            addNode.NodeInputs[0].InputProviderPin = new OutputPin(addNode.NodeInputs[0].DataType, null) { OutputValue = new DEBUGINTEGER(3) };
            addNode.NodeInputs[1].InputProviderPin = new OutputPin(addNode.NodeInputs[1].DataType, null) { OutputValue = new DEBUGINTEGER(6) };

            addNode.CalculateOutput();

            // Second node
            GraphNode addNode2 = new GraphNode(typeof(AddNode));

            addNode2.NodeInputs[0].InputProviderPin = addNode.NodeOutputs[0];
            addNode2.NodeInputs[1].InputProviderPin = new OutputPin(addNode2.NodeInputs[1].DataType, null) { OutputValue = new DEBUGINTEGER(1) };

            addNode2.CalculateOutput();

            Console.WriteLine(addNode2.NodeOutputs[0].OutputValue.GetDataAsString());

            Console.ReadLine();
        }
    }
}
