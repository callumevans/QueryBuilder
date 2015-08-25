using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.Conversion
{
    [NodeName("Bool to String", true)]
    [NodeCategory("Converters")]
    [ConversionRule(typeof(DataTypes.Boolean), typeof(DataTypes.String))]
    public class BoolToString : NodeBase
    {
        [ExposedInput]
        public DataTypes.Boolean input;

        [ExposedOutput]
        public DataTypes.String output;

        public override void NodeFunction()
        {
            output = new DataTypes.String(input.GetDataAsString());
        }
    }
}
