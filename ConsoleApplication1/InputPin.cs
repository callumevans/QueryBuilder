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
        public OutputPin InputProviderPin
        {
            get
            {
                foreach (PinConnection connection in Parent.GraphManager.Connections)
                {
                    if (connection.InputPin == this)
                        return connection.OutputPin;
                }

                return null;
            }
        }

        public IDataTypeContainer GetValue
        {
            get { return InputProviderPin.OutputValue; }
        }

        public InputPin(Type inputType, GraphNode parent) : base(inputType, parent) { }
    }
}
