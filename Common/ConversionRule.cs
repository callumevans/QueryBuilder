using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConversionRule : Attribute
    {
        public Type InputType { get; private set; }
        public Type OutputType { get; private set; }

        public ConversionRule(Type inputType, Type outputType)
        {
            InputType = inputType;
            OutputType = outputType;
        }
    }
}
