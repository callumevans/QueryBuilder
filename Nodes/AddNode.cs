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
        public DEBUGINTEGER inputOneloooooong;

        [ExposedInput(1)]
        public DEBUGINTEGER inputTwo;

        [ExposedOutput(0)]
        public DEBUGINTEGER outputOne;

        public override void NodeFunction()
        {
            outputOne = new DEBUGINTEGER(inputOneloooooong.value + inputTwo.value);
        }
    }
}
