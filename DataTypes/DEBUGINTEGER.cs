using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public struct DEBUGINTEGER : IDataTypeContainer, IValueData
    {
        public int value;

        public DEBUGINTEGER(int val)
        {
            this.value = val;
        }

        public void SetValue(string value)
        {
            this.value = Convert.ToInt32(value);
        }
    }
}
