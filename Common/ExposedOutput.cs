using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExposedOutput : Attribute
    {
        public int OutputOrder { get; set; }

        public ExposedOutput(int order)
        {
            OutputOrder = order;
        }
    }
}
