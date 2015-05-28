using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataTypes;

namespace Nodes
{
    [NodeAttributes(
       inputs: new Type[] { },
       outputs: new Type[] { })]
    public class StartNode : ExecutableNode
    {
        public StartNode(QueryState state) : base(state) { }

        public override IList<IDataTypeContainer> NodeFunction(IList<IDataTypeContainer> inputs)
        {
            return new List<IDataTypeContainer>();
        }

        public override int GetExecutionPath()
        {
            return 1;
        }

        public override int GetOutgoingExecutionCount()
        {
            return 1;
        }
    }
}
