using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.HTML
{
    [NodeName("HTML Head")]
    [NodeCategory("HTML")]
    [ExecutionOutDescription(0, "Out")]
    public class HTMLHead : ExecutableNode
    {
        [ExposedInput(LabelDisplay.Custom, "Title")]
        public DataTypes.String title;

        public HTMLHead(QueryState state) : base(state) { }

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
            htmlOutput += "<head>\n";

            htmlOutput += "<title>" + title.GetDataAsString() + "</title>\n";

            htmlOutput += "</head>\n";

            State.VariableBag["HTML"] = htmlOutput;
        }
    }
}
