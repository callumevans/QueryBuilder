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
        public DataTypes.Boolean inputBool;

        [ExposedOutput]
        public DataTypes.String outputString;

        public override void NodeFunction()
        {
            outputString = new DataTypes.String(inputBool.GetDataAsString());
        }
    }
}
