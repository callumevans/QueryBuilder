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
using Fluent;
using VisualQueryApplication.Controls.GraphBuilder;

namespace VisualQueryApplication.ViewModels
{
    public class VisualNodeViewModel : VisualGraphComponentViewModel
    {
        public Type NodeType { get; private set; }

        public string NodeTitle
        {
            get
            {
                Attribute titleAttribute = NodeType.GetCustomAttribute(typeof(NodeName));
                return ((NodeName)titleAttribute).Name;
            }
        }

        public ObservableCollection<NodePinViewModel> Inputs
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

        private ObservableCollection<NodePinViewModel> inputs = new ObservableCollection<NodePinViewModel>();

        public ObservableCollection<NodePinViewModel> Outputs
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

        private ObservableCollection<NodePinViewModel> outputs = new ObservableCollection<NodePinViewModel>();

        public ObservableCollection<NodePinViewModel> ExecutionInputs
        {
            get { return executionInputs; }
            set
            {
                executionInputs = value;
                OnPropertyChanged(nameof(ExecutionInputs));
            }
        } 

        private ObservableCollection<NodePinViewModel> executionInputs = new ObservableCollection<NodePinViewModel>(); 

        public ObservableCollection<NodePinViewModel> ExecutionOutputs
        {
            get { return executionOutputs; }
            set
            {
                executionOutputs = value;
                OnPropertyChanged(nameof(ExecutionOutputs));
            }
        }

        private ObservableCollection<NodePinViewModel> executionOutputs = new ObservableCollection<NodePinViewModel>(); 

        public VisualNodeViewModel(Type nodeType)
        {
            this.NodeType = nodeType;

            // If the node is executable
            if (this.NodeType.IsSubclassOf(typeof (ExecutableNode)))
            {
                // Set an execution-in pin
                executionInputs.Add(new NodePinViewModel("In", null, false, true));

                // Import execution-out pins
                foreach (Attribute attribute in this.NodeType.GetCustomAttributes(typeof (ExecutionOutDescription)))
                {
                    executionOutputs.Add(new NodePinViewModel(((ExecutionOutDescription)attribute).Label, null, true, true));
                }
            }

            // Import inputs and outputs
            foreach (FieldInfo field in this.NodeType.GetFields())
            {
                foreach (Attribute attribute in field.GetCustomAttributes())
                {
                    if (attribute.GetType() == typeof(ExposedInput))
                        inputs.Add(new NodePinViewModel(field.Name, field.FieldType, false, false));
                    else if (attribute.GetType() == typeof(ExposedOutput))
                        outputs.Add(new NodePinViewModel(field.Name, field.FieldType, true, false));
                }
            }
        }

        public override void RemoveConnections()
        {
            GraphEditorViewModel editor = (GraphEditorViewModel)((MainWindow)(App.Current.MainWindow)).VisualEditor.DataContext;
            List<NodePin> pins = new List<NodePin>();

            pins.AddRange(Inputs.Select(pin => pin.Pin));
            pins.AddRange(Outputs.Select(pin => pin.Pin));

            List<ConnectionViewModel> connectionsToRemove = new List<ConnectionViewModel>();

            foreach (var connection in editor.Connections)
            {
                if (pins.Contains(connection.InputPin) || (pins.Contains(connection.OutputPin)))
                {
                    connectionsToRemove.Add(connection);
                }
            }

            foreach (var connection in connectionsToRemove)
            {
                editor.Connections.Remove(connection);
            }
        }
    }
}
