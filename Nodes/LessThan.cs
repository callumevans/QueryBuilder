using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataTypes;

namespace Nodes
{
    [NodeName("Less Than")]
    public class LessThan : NodeBase
    {
        [ExposedInput]
        public DataTypes.Integer inputOne;

        [ExposedInput]
        public DataTypes.Integer inputTwo;

        [ExposedOutput]
        public DataTypes.Boolean outputOne;

        public override void NodeFunction()
        {
            outputOne = new DataTypes.Boolean(inputOne.value < inputTwo.value);
        }
    }
}
