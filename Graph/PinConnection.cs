using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class PinConnection
    {
        public OutputPin OutputPin { get; set; }
        public InputPin InputPin { get; set; }

        public PinConnection(OutputPin outputPin, InputPin inputPin)
        {
            OutputPin = outputPin;
            InputPin = inputPin;
        }
    }
}
