using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.HTML.Pure
{
    [NodeName("Paragraph")]
    [NodeCategory("HTML")]
    public class Paragraph : NodeBase
    {
        [ExposedInput(LabelDisplay.Custom, "Paragraph Content")]
        public DataTypes.String content;

        [ExposedOutput(LabelDisplay.Hidden)]
        public DataTypes.String output;

        public override void NodeFunction()
        {
            string outString = "<p>"+ content.GetDataAsString() + "</p>\n";
            output = new DataTypes.String(outString);
        }
    }
}
