using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataTypes;

namespace Nodes
{
    [NodeName("Start")]
    [ExecutionOutDescription(0, "Out")]
    public class StartNode : ExecutableNode
    {
        public StartNode(QueryState state) : base(state) { }

        public override int GetExecutionPath()
        {
            return 0;
        }

        public override void NodeFunction()
        {
            return;
        }
    }
}
