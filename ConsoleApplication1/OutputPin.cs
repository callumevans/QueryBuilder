using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class OutputPin : BaseNodePin
    {
        public bool OutputRealised { get; set; }
        public List<InputPin> Connections { get; set; }
        public IDataTypeContainer OutputValue { get; set; }

        public OutputPin(Type outputType, GraphNode parent) : base(outputType, parent)
        {
            OutputRealised = false;
            OutputValue = (IDataTypeContainer)Activator.CreateInstance(outputType);
        }
    }
}
