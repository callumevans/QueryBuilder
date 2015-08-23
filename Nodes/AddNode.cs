using Common;
using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    [NodeName("Add")]
    [NodeCategory("Maths")]
    public class AddNode : NodeBase
    {
        [ExposedInput]
        public DataTypes.Integer inputOne;

        [ExposedInput]
        public DataTypes.Integer inputTwo;

        [ExposedOutput]
        public DataTypes.Integer outputOne;

        public override void NodeFunction()
        {
            outputOne = new DataTypes.Integer(inputOne.value + inputTwo.value);
        }
    }
}
