using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.String
{
    [NodeName("Concatenate")]
    [NodeCategory("String")]
    public class Concatenate : NodeBase
    {
        [ExposedInput]
        public DataTypes.String A;

        [ExposedInput]
        public DataTypes.String B;

        [ExposedOutput]
        public DataTypes.String output;

        public override void NodeFunction()
        {
            output = new DataTypes.String(A.GetDataAsString() + B.GetDataAsString());
        }
    }
}
