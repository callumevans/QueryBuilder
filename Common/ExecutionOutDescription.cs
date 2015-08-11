using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExecutionOutDescription : Attribute
    {
        public int Order { get; private set; }
        public string Label { get; private set; }

        public ExecutionOutDescription(int order, string label)
        {
            Order = order;
            Label = label;
        }
    }
}
