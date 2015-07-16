using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualQueryApplication.Controls.GraphBuilder;

namespace VisualQueryApplication.Model
{
    public class PinModel
    {
        public string Name { get; set; }
        public NodePin Pin { get; set; }
        public Type DataType { get; set; }

        public PinModel(string name, Type dataType, NodePin pin = null)
        {
            this.Name = name;
            this.DataType = dataType;
            this.Pin = pin;
        }
    }
}
