using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.Debug
{
    [NodeName("Print")]
    [NodeCategory("Debug")]
    [ExecutionOutDescription(0, "Out")]
    public class PrintNode : ExecutableNode
    {
        [ExposedInput(LabelDisplay.Custom, "Print")]
        public DataTypes.String print;

        public PrintNode(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            return 0;
        }

        public override void NodeFunction()
        {
            string debugOutput = "";

            // 'DebugPrint' is the output's string key
            if (State.VariableBag.ContainsKey("DebugPrint") == false)
            {
                State.VariableBag.Add("DebugPrint", "");
            }

            debugOutput = State.VariableBag["DebugPrint"].ToString();
            debugOutput += print.GetDataAsString();

            State.VariableBag["DebugPrint"] = debugOutput;
        }
    }
}
