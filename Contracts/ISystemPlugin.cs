﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISystemPlugin
    {
        string Name { get; }
        string Description { get; }
    }
}
