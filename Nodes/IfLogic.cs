using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes
{
    [NodeName("If")]
    [ExecutionOutDescription(0, "True")]
    [ExecutionOutDescription(1, "False")]
    public class IfLogic : ExecutableNode
    {
        [ExposedInput]
        public DataTypes.Boolean boolValue;

        public IfLogic(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            bool val = Boolean.Parse(boolValue.GetDataAsString());

            if (val)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public override void NodeFunction()
        {
        }
    }
}
