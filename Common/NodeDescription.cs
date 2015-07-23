using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeDescription : Attribute
    {
        public string Description { get; set; }

        public NodeDescription(string description)
        {
            Description = description;
        }
    }
}
