using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class InputPin : BaseNodePin
    {
        public OutputPin InputProviderPin { get; set; }

        public IDataTypeContainer GetValue
        {
            get
            {
                return InputProviderPin.OutputValue;
            }
        }

        public InputPin(Type inputType, GraphNode parent, OutputPin inputProvider = null) : base(inputType, parent)
        {
            InputProviderPin = inputProvider;
        }
    }
}
