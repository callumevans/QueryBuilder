using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public class NodeAttributes : Attribute
    {
        public List<Type> Inputs { get; set; }
        public List<Type> Outputs { get; set; }
        public bool HasExecution { get; set; }

        public NodeAttributes(Type[] inputs, Type[] outputs, bool hasExecution)
        {
            Inputs = inputs.ToList();
            Outputs = outputs.ToList();
            HasExecution = hasExecution;
        }
    }
}
