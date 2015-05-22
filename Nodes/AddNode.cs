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
        inputs:  new Type[] { typeof(DEBUGINTEGER), typeof(DEBUGINTEGER) },
        outputs: new Type[] { typeof(DEBUGINTEGER) })]
    public class AddNode : VisualNodeBase
    {
        public override IList<IDataTypeContainer> NodeFunction(IList<IDataTypeContainer> inputs, QueryState state)
        {
            IList<IDataTypeContainer> outputs = new List<IDataTypeContainer>();

            DEBUGINTEGER a = (DEBUGINTEGER)inputs[0];
            DEBUGINTEGER b = (DEBUGINTEGER)inputs[1];

            outputs.Add(new DEBUGINTEGER(a.value + b.value));

            return outputs;
        }
    }
}
