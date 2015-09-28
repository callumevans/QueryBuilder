using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.HTML
{
    [NodeName("HTML End")]
    [NodeCategory("HTML")]
    [ExecutionOutDescription(0, "Out")]
    public class HTMLEnd : ExecutableNode
    {
        public HTMLEnd(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            return 0;
        }

        public override void NodeFunction()
        {
            string htmlOutput = "";

            if (State.VariableBag.ContainsKey("HTML") == false)
            {
                State.VariableBag.Add("HTML", "");
            }

            htmlOutput = State.VariableBag["HTML"].ToString();
            htmlOutput += "\n</html>";

            State.VariableBag["HTML"] = htmlOutput;
        }
    }
}