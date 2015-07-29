using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Boolean : IDataTypeContainer
    {
        public bool value;

        public Boolean(bool val)
        {
            this.value = val;
        }

        public string GetDataAsString()
        {
            return this.value.ToString();
        }

        public void SetValue(object value)
        {
            this.value = Convert.ToBoolean(value.ToString());
        }
    }
}
