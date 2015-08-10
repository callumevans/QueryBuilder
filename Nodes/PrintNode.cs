using Common;
using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    [NodeName("Print")]
    public class PrintNode : ExecutableNode
    {
        [ExposedInput(LabelDisplay.Custom, "Print Value")]
        public DataTypes.Integer printValue;

        public PrintNode(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            return 0;
        }

        public override void NodeFunction()
        {
            State.VariableBag.Add("Print Function", printValue.GetDataAsString());
        }
    }
}
