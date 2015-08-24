using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeName : Attribute
    {
        public string Name { get; private set; }
        public bool IsHidden { get; private set; }

        public NodeName(string name, bool hidden = false)
        {
            Name = name;
            IsHidden = hidden;
        }
    }
}
