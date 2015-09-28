using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.HTML.Pure
{
    [NodeName("Div")]
    [NodeCategory("HTML")]
    public class Div : NodeBase
    {
        [ExposedInput(LabelDisplay.Custom, "Div Content")]
        public DataTypes.String content;

        [ExposedOutput(LabelDisplay.Hidden)]
        public DataTypes.String output;

        public override void NodeFunction()
        {
            string outString = "<div>\n" + content.GetDataAsString() + "\n</div>";
            output = new DataTypes.String(outString);
        }
    }
}
