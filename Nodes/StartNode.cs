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
    public class StartNode : VisualNodeBase, IHasExecution
    {
        public override IList<IDataTypeContainer> NodeFunction(IList<IDataTypeContainer> inputs, QueryState state)
        {
            return null;
        }

        public int GetExecutionPath()
        {
            return 1;
        }

        public int GetOutgoingExecutionCount()
        {
            return 1;
        }
    }
}
