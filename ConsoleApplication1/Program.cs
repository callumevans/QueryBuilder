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
            GraphNode addNode = new GraphNode(typeof(AddNode));

            addNode.NodeInputs[0].InputProviderPin = new OutputPin(addNode.NodeInputs[0].DataType, null) { OutputValue = new DEBUGINTEGER(3) };
            addNode.NodeInputs[1].InputProviderPin = new OutputPin(addNode.NodeInputs[1].DataType, null) { OutputValue = new DEBUGINTEGER(6) };

            addNode.CalculateOutput();

            Console.WriteLine(addNode.NodeOutputs[0].OutputValue.GetDataAsString());

            Console.ReadLine();
        }

        static void InitNode(Type t, params object[] inputs)
        {
            NodeAttributes na = (NodeAttributes)Attribute.GetCustomAttribute(t, typeof(NodeAttributes));

            IList<IDataTypeContainer> nodeInputs = new List<IDataTypeContainer>();

            for (int i = 0; i < na.Inputs.Count; i++)
            {
                IDataTypeContainer instance = (IDataTypeContainer)Activator.CreateInstance(na.Inputs[i]);
                instance.SetValue(inputs[i]);

                nodeInputs.Add(instance);
            }

            VisualNodeBase classInstance = (VisualNodeBase)Activator.CreateInstance(t);
            List<IDataTypeContainer> result = (List<IDataTypeContainer>)classInstance.NodeFunction(nodeInputs);

            Console.WriteLine(result[0].GetDataAsString());
        }
    }
}
