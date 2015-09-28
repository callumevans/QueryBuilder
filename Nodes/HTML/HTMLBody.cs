using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.HTML
{
    [NodeName("HTML Body")]
    [NodeCategory("HTML")]
    [ExecutionOutDescription(0, "Out")]
    public class HTMLBody : ExecutableNode
    {
        [ExposedInput(LabelDisplay.Custom, "Body Content")]
        public DataTypes.String bodyHTML;

        public HTMLBody(QueryState state) : base(state) { }

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

            htmlOutput += "<body>\n";
            htmlOutput += bodyHTML.GetDataAsString();
            htmlOutput += "\n</body>";

            State.VariableBag["HTML"] = htmlOutput;
        }
    }
}
