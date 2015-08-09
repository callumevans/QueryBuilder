using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class String : IDataTypeContainer
    {
        public string value;

        public String()
        {
        }

        public String(string val)
        {
            this.value = val;
        }

        public string GetDataAsString()
        {
            return this.value;
        }

        public void SetValue(object value)
        {
            this.value = Convert.ToString(value);
        }
    }
}
