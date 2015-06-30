using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualQueryApplication.Model
{
    public class VisualNodeModel
    {
        public Type NodeType { get; set; }

        public VisualNodeModel(Type nodeType)
        {
            this.NodeType = nodeType;
        }
    }
}
