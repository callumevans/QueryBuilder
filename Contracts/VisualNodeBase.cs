using Common;
using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public abstract class VisualNodeBase : ISystemPlugin
    {
        public virtual string Name { get; }
        public virtual string Description { get; }

        public VisualNodeBase()
        {

        }

        public abstract IList<IDataTypeContainer> NodeFunction(IList<IDataTypeContainer> inputs, QueryState state);
    }
}
