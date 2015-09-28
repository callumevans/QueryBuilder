﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Nodes.Maths
{
    [NodeName("Add")]
    [NodeCategory("Maths")]
    public class Add : NodeBase
    {
        [ExposedInput]
        public DataTypes.Numeric A;

        [ExposedInput]
        public DataTypes.Numeric B;

        [ExposedOutput]
        public DataTypes.Numeric output;

        public override void NodeFunction()
        {
            output = new DataTypes.Numeric(A.value + B.value);
        }
    }
}
