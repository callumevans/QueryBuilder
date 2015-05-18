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

            // DEBUG CONNECTIONS
            GraphNodeConnection input1 = new GraphNodeConnection(addNode.NodeInputs[0].Type, addNode.NodeInputs[0]);
            input1.ValueIsRealised = true;
            input1.Value.SetValue("1");

            GraphNodeConnection input2 = new GraphNodeConnection(addNode.NodeInputs[1].Type, addNode.NodeInputs[1]);
            input2.ValueIsRealised = true;
            input2.Value.SetValue("4");

            //

            addNode.NodeInputs[0].Connection = input1;
            addNode.NodeInputs[1].Connection = input2;

            //

            addNode.CalculateOutput();

            Console.WriteLine(addNode.NodeOutputs[0].Connection.Value.GetDataAsString());

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
