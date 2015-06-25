using Common;
using DataTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisualQueryApplication.Model;

namespace VisualQueryApplication.ViewModels
{
    public class VisualGraphViewModel : ViewModelBase
    {
        public Type NodeType
        {
            get
            {
                return nodeType;
            }
            set
            {
                SetValue(ref nodeType, value);
            }
        }

        private Type nodeType;

        public string NodeTitle
        {
            get
            {
                Attribute titleAttribute = nodeType.GetCustomAttribute(typeof(NodeName));
                return ((NodeName)titleAttribute).Name;
            }
        }

        public List<IDataTypeContainer> Inputs
        {
            get
            {
                return inputs;
            }
            set
            {
                SetValue(ref inputs, value);
            }
        }

        private List<IDataTypeContainer> inputs;

        public VisualGraphViewModel(Type type)
        {
            nodeType = type;
            inputs = new List<IDataTypeContainer>();
        }
    }
}
