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
            InitNode(typeof(AddNode));
            Console.ReadLine();
        }

        static void InitNode(Type t)
        {
            NodeAttributes na = (NodeAttributes)Attribute.GetCustomAttribute(t, typeof(NodeAttributes));

            IList<IDataTypeContainer> nodeInputs = new List<IDataTypeContainer>();

            foreach (Type item in na.Inputs)
            {
                var instance = Activator.CreateInstance(item);
                nodeInputs.Add((IDataTypeContainer)instance);
            }

            MethodInfo method = t.GetMethod("NodeFunction");
            VisualNodeBase classInstance = (VisualNodeBase)Activator.CreateInstance(t);

            var result = classInstance.NodeFunction(nodeInputs);

            Console.WriteLine(result);
        }
    }
}
