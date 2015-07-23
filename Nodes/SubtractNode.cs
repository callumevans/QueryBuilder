using Common;
using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    [NodeName("Subtract")]
    [NodeDescription("Subtracts two values")]
    public class SubtractNode : NodeBase
    {
        [ExposedInput(0)]
        public DEBUGINTEGER inputOne;

        [ExposedInput(1)]
        public DEBUGINTEGER inputTwo;

        [ExposedOutput(0)]
        public DEBUGINTEGER outputOne;

        public override void NodeFunction()
        {
            outputOne = new DEBUGINTEGER(inputOne.value - inputTwo.value);
        }
    }
}
