using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataTypes;

namespace Nodes.Conversion
{
    [NodeName("Integer to String", true)]
    [NodeCategory("Converters")]
    [ConversionRule(typeof(DataTypes.Integer), typeof(DataTypes.String))]
    public class IntegerToString : NodeBase
    {
        [ExposedInput]
        public DataTypes.Integer inputInteger;

        [ExposedOutput]
        public DataTypes.String outputString;

        public override void NodeFunction()
        {
            outputString = new DataTypes.String(inputInteger.GetDataAsString());
        }
    }
}
