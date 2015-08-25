using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataTypes;

namespace Nodes.Conversion
{
    [NodeName("Numeric to String", true)]
    [NodeCategory("Converters")]
    [ConversionRule(typeof(DataTypes.Numeric), typeof(DataTypes.String))]
    public class NumericToString : NodeBase
    {
        [ExposedInput]
        public DataTypes.Numeric input;

        [ExposedOutput]
        public DataTypes.String output;

        public override void NodeFunction()
        {
            output = new DataTypes.String(input.GetDataAsString());
        }
    }
}
