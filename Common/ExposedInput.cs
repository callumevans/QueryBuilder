using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExposedInput : Attribute
    {
        public int InputOrder { get; set; }

        public ExposedInput(int order)
        {
            InputOrder = order;
        }
    }
}
