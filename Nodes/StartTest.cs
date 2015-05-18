using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;

namespace Nodes
{
    [NodeAttributes(
    inputs: new Type[] { },
    outputs: new Type[] { })]
    public class StartTest : VisualNodeBase, IHasExecution
    {
        public IHasExecution benignTestNode;

        public override IList<IDataTypeContainer> NodeFunction(IList<IDataTypeContainer> inputs)
        {
            IList<IDataTypeContainer> output = new List<IDataTypeContainer>();
            return output;
        }

        public IHasExecution GetExecutionParent()
        {
            return null;
        }

        public IHasExecution GetExecutionTarget()
        {
            throw new NotImplementedException();
        }

        public void SetExecutionParent(IHasExecution parent)
        {
            throw new NotImplementedException();
        }

        public void SetExecutionTarget(IHasExecution target)
        {
            throw new NotImplementedException();
        }
    }
}
