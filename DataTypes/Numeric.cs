using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Numeric : IDataTypeContainer
    {
        public double value;

        public Numeric()
        {
        }

        public Numeric(double val)
        {
            this.value = val;
        }

        public string GetDataAsString()
        {
            return this.value.ToString();
        }

        public void SetValue(object value)
        {
            this.value = Double.Parse(value.ToString());
        }
    }
}
