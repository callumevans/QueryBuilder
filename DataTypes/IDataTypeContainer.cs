using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    /// <summary>
    /// Defines a DataType for use in the graph system.
    /// </summary>
    public interface IDataTypeContainer
    {
        void SetValue(object value);
        string GetDataAsString();
    }
}
