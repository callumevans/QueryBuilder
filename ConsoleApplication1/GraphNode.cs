using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GraphNode
    {
        public List<GraphNodePin> Inputs { get; set; }
        public List<GraphNodePin> Outputs { get; set; }

        public GraphNode()
        {

        }

        public class GraphNodeConnection
        {
            public bool ValueIsRealised { get; set; }
            public object Value { get; set; }

            public GraphNodePin RootPin { get; set; }
            public GraphNodePin TargetPin { get; set; }

            public GraphNodeConnection(GraphNodePin rootPin, GraphNodePin targetPin)
            {
                RootPin = rootPin;
                TargetPin = targetPin;

                ValueIsRealised = false;
                Value = null;
            }
        }

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
        }

        public enum PinType { OUTPUT, INPUT }
    }
}
