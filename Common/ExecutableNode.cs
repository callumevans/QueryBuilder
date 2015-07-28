using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;

namespace Common
{
    public abstract class ExecutableNode : NodeBase
    {
        protected QueryState State { get; private set; }

        public ExecutableNode(QueryState state)
        {
            this.State = state;
        }

        public abstract int GetExecutionPath();
    }
}
