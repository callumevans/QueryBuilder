using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.Boolean_Logic
{
    [NodeName("NOT")]
    [NodeCategory("Boolean Logic")]
    public class NOT : NodeBase
    {
        [ExposedInput(LabelDisplay.Hidden)]
        public DataTypes.Boolean input;

        [ExposedOutput(LabelDisplay.Hidden)]
        public DataTypes.Boolean output;

        public override void NodeFunction()
        {
            output = new DataTypes.Boolean(!input.value);
        }
    }
}
