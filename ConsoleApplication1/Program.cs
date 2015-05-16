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
            InitNode(typeof(AddNode), "3", "5");
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

            MethodInfo method = t.GetMethod("NodeFunction");
            VisualNodeBase classInstance = (VisualNodeBase)Activator.CreateInstance(t);

            List<IDataTypeContainer> result = (List<IDataTypeContainer>)classInstance.NodeFunction(nodeInputs);

            Console.WriteLine(result[0].ToString());
        }
    }
}
