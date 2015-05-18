using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GraphNodePin
    {
        public Type Type { get; }
        public GraphNode Parent { get; }
        public GraphNodeConnection Connection { get; set; }
        public PinType PinType { get; }

        public GraphNodePin(Type type, GraphNode parent, PinType pinType)
        {
            Type = type;
            Parent = parent;
            PinType = pinType;
        }

        public void AddConnection(GraphNodePin otherPin)
        {
            GraphNodeConnection connection;

            if (PinType == PinType.INPUT)
                connection = new GraphNodeConnection(this, otherPin);
            else
                connection = new GraphNodeConnection(otherPin, this);
        }
    }

    public enum PinType { OUTPUT, INPUT }

}
