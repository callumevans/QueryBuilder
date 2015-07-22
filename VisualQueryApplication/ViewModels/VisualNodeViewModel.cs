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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VisualQueryApplication.Controls.GraphBuilder;
using VisualQueryApplication.Model;

namespace VisualQueryApplication.ViewModels
{
    public class VisualNodeViewModel : VisualGraphComponentViewModel
    {
        private VisualNodeModel nodeModel;

        public string NodeTitle
        {
            get
            {
                Attribute titleAttribute = nodeModel.NodeType.GetCustomAttribute(typeof(NodeName));
                return ((NodeName)titleAttribute).Name;
            }
        }

        public ObservableCollection<PinModel> Inputs
        {
            get
            {
                return inputs;
            }
            set
            {
                inputs = value;
                OnPropertyChanged(nameof(Inputs));
            }
        }

        private ObservableCollection<PinModel> inputs = new ObservableCollection<PinModel>();

        public ObservableCollection<PinModel> Outputs
        {
            get
            {
                return outputs;
            }
            set
            {
                outputs = value;
                OnPropertyChanged(nameof(Outputs));
            }
        }

        private ObservableCollection<PinModel> outputs = new ObservableCollection<PinModel>();

        public VisualNodeViewModel(Type nodeType)
        {
            nodeModel = new VisualNodeModel(nodeType);

            // Import inputs and outputs
            foreach (FieldInfo field in nodeModel.NodeType.GetFields())
            {
                foreach (Attribute attribute in field.GetCustomAttributes())
                {
                    if (attribute.GetType() == typeof(ExposedInput))
                        inputs.Add(new PinModel(field.Name, field.FieldType));
                    else if (attribute.GetType() == typeof(ExposedOutput))
                        outputs.Add(new PinModel(field.Name, field.FieldType));
                }
            }
        }

    }
}
