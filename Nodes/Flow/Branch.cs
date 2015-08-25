using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.Flow
{
    [NodeName("Branch")]
    [NodeCategory("Flow")]
    [ExecutionOutDescription(0, "True")]
    [ExecutionOutDescription(1, "False")]
    public class Branch : ExecutableNode
    {
        [ExposedInput(LabelDisplay.Hidden)]
        public DataTypes.Boolean condition;

        public Branch(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            if (condition.value)
                return 0;
            else
                return 1;
        }

        public override void NodeFunction()
        {
        }
    }
}
