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
    [NodeDescription("Adds two numbers")]
    public class AddNode : NodeBase
    {
        [ExposedInput(0)]
        public DataTypes.Integer inputOne;

        [ExposedInput(1)]
        public DataTypes.Integer inputTwo;

        [ExposedOutput(0)]
        public DataTypes.Integer outputOne;

        public override void NodeFunction()
        {
            outputOne = new DataTypes.Integer(inputOne.value + inputTwo.value);
        }
    }
}
