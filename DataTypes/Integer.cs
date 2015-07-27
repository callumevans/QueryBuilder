using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Integer : IDataTypeContainer
    {
        public int value;

        public Integer(int val)
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
