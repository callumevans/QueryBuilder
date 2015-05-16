using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    interface IValueData : IDataTypeContainer
    {
        void SetValue(string value);
    }
}
