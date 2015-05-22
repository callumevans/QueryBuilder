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
        public List<string> InputLabels { get; set; }

        public List<Type> Outputs { get; set; }
        public List<string> OutputLabels { get; set; }

        public NodeAttributes(Type[] inputs, Type[] outputs, string[] inputLabels = null, string[] outputLabels = null)
        {
            Inputs = inputs.ToList();
            Outputs = outputs.ToList();
        }
    }
}
