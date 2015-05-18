using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IHasExecution
    {
        IHasExecution GetExecutionParent();
        IHasExecution GetExecutionTarget();

        void SetExecutionParent(IHasExecution parent);
        void SetExecutionTarget(IHasExecution target);
    }
}
