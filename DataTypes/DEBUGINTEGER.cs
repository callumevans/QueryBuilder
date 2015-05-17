﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public struct DEBUGINTEGER : IDataTypeContainer
    {
        public int value;

        public DEBUGINTEGER(int val)
        {
            this.value = val;
        }

        public string GetDataAsString()
        {
            return this.value.ToString();
        }

        public void SetValue(object value)
        {
            this.value = Convert.ToInt32(value.ToString());
        }
    }
}
