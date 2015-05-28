using Common;
using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    [NodeAttributes(
        inputs: new Type[] { typeof(DEBUGINTEGER) },
        outputs: new Type[] { })]
    public class EndNode : ExecutableNode
    {
        public EndNode(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            return 0;
        }

        public override int GetOutgoingExecutionCount()
        {
            return 0;
        }

        public override IList<IDataTypeContainer> NodeFunction(IList<IDataTypeContainer> inputs)
        {
            IList<IDataTypeContainer> outputs = new List<IDataTypeContainer>();

            DEBUGINTEGER a = (DEBUGINTEGER)inputs[0];

            Console.WriteLine("End Node Printout Function: " + a.GetDataAsString());

            return outputs;
        }
    }
}
