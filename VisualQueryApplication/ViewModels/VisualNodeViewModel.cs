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
using System.Windows.Input;
using VisualQueryApplication.Model;

namespace VisualQueryApplication.ViewModels
{
    public class VisualNodeViewModel : ViewModelBase
    {
        private VisualNodeModel nodeModel;

        public List<GraphConnectionViewModel> Connections
        {
            get
            {
                return connectionsViewModel;
            }
            set
            {
                connectionsViewModel = value;
                OnPropertyChanged("Connections");
            }
        }

        private List<GraphConnectionViewModel> connectionsViewModel = new List<GraphConnectionViewModel>();

        public int ZIndex
        {
            get
            {
                return zIndex;
            }
            set
            {
                zIndex = value;
                OnPropertyChanged("ZIndex");
            }
        }

        private int zIndex = 0;

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }

        private double x = 20;

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }

        private double y = 20;

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

        public Dictionary<string, Type> Inputs
        {
            get
            {
                inputs = new Dictionary<string, Type>();

                foreach (FieldInfo field in nodeModel.NodeType.GetFields())
                {
                    foreach (Attribute attribute in field.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(ExposedInput))
                            inputs.Add(field.Name, field.GetType());
                    }
                }

                return inputs;
            }
        }

        private Dictionary<string, Type> inputs;

        public Dictionary<string, Type> Outputs
        {
            get
            {
                outputs = new Dictionary<string, Type>();

                foreach (FieldInfo field in nodeModel.NodeType.GetFields())
                {
                    foreach (Attribute attribute in field.GetCustomAttributes())
                    {
                        if (attribute.GetType() == typeof(ExposedOutput))
                            outputs.Add(field.Name, field.GetType());
                    }
                }

                return outputs;
            }
        }

        private Dictionary<string, Type> outputs;

        public VisualNodeViewModel(Type type)
        {
            nodeType = type;
            nodeModel = new VisualNodeModel(type);
        }
    }
}
