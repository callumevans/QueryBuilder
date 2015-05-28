using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class NodeBase
    {
        public abstract IList<IDataTypeContainer> NodeFunction(IList<IDataTypeContainer> inputs);
    }
}
