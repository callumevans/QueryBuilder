using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public interface IDataTypeContainer
    {
        void SetValue(object value);
        string GetDataAsString();
    }
}
