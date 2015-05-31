using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualQueryBuilder.Graph
{
    public class OutputPin : BaseNodePin
    {
        public bool OutputRealised { get; set; }

        public List<InputPin> Connections
        {
            get
            {
                List<InputPin> returnConnections = new List<InputPin>();

                foreach (PinConnection connection in Parent.GraphManager.Connections)
                {
                    if (connection.OutputPin == this)
                    {
                        returnConnections.Add(connection.InputPin);
                    }
                }

                return returnConnections;
            }
        }

        public IDataTypeContainer OutputValue { get; set; }

        public OutputPin(Type outputType, GraphNode parent) : base(outputType, parent)
        {
            OutputRealised = false;
            OutputValue = (IDataTypeContainer)Activator.CreateInstance(outputType);
        }
    }
}
