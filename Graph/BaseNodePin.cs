using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public abstract class BaseNodePin
    {
        public Type DataType { get; }
        public GraphNode Parent { get; }

        public BaseNodePin(Type type, GraphNode parent)
        {
            DataType = type;
            Parent = parent;
        }
    }
}
