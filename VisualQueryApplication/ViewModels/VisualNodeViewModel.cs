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

        public List<FieldInfo> Inputs
        {
            get
            {
                List<FieldInfo> inputs = new List<FieldInfo>();

                foreach (FieldInfo field in nodeType.GetFields())
                {
                    foreach (Attribute attribute in field.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(ExposedInput))
                            inputs.Add(field);
                    }
                }

                return inputs;
            }
        }

        public List<FieldInfo> Outputs
        {
            get
            {
                List<FieldInfo> outputs = new List<FieldInfo>();

                foreach (FieldInfo field in nodeType.GetFields())
                {
                    foreach (Attribute attribute in field.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(ExposedOutput))
                            outputs.Add(field);
                    }
                }

                return outputs;
            }
        }

        public VisualGraphViewModel(Type type)
        {
            nodeType = type;
        }
    }
}
