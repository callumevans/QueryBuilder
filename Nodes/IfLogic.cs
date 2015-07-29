using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes
{
    [NodeName("If")]
    [NodeDescription("")]
    [ExecutionOutDescription(1, "True")]
    [ExecutionOutDescription(2, "False")]
    public class IfLogic : ExecutableNode
    {
        [ExposedInput(0)]
        public DataTypes.Boolean boolValue;

        public IfLogic(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            bool val = Boolean.Parse(boolValue.GetDataAsString());

            if (val)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public override void NodeFunction()
        {
        }
    }
}
