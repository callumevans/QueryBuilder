using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeCategory : Attribute
    {
        public string Category { get; private set; }

        public NodeCategory(string category)
        {
            this.Category = category;
        }
    }
}
