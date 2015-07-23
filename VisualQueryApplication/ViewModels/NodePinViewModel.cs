using DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VisualQueryApplication.Controls.GraphBuilder;
using VisualQueryApplication.Model;

namespace VisualQueryApplication.ViewModels
{
    public class NodePinViewModel : ViewModelBase
    {
        public bool IsOutputPin { get; set; }
        public NodePin Pin { get; private set; }

        public Type DataType
        {
            get { return dataType; }
            set
            {
                dataType = value;
                OnPropertyChanged("DataType");

                // Update pin colour
                if (value == typeof(DEBUGINTEGER))
                    PinColour = new SolidColorBrush(Colors.Green);
            }
        }

        private Type dataType;

        public SolidColorBrush PinStroke
        {
            get { return pinStroke; }
            set
            {
                pinStroke = value;
                OnPropertyChanged(nameof(PinStroke));
            }
        }

        private SolidColorBrush pinStroke = new SolidColorBrush(Colors.Black);

        public SolidColorBrush PinColour
        {
            get { return pinColour; }
            set
            {
                pinColour = value;
                OnPropertyChanged(nameof(PinColour));
            }
        }

        private SolidColorBrush pinColour = new SolidColorBrush(Colors.Gray);

        public UserControl ParentNode
        {
            get { return parentNode; }
            set
            {
                parentNode = value;
                OnPropertyChanged(nameof(ParentNode));
            }
        }

        public UserControl parentNode;

        public ICommand AllocatePinToInputCommand
        {
            get { return allocatePinToInputCommand; }
            set
            {
                allocatePinToInputCommand = value;
                OnPropertyChanged(nameof(AllocatePinToInputCommand));
            }
        }

        private ICommand allocatePinToInputCommand;

        public ICommand RemoveConnectionsCommand
        {
            get { return removeConnectionsCommand; }
            set
            {
                removeConnectionsCommand = value;
                OnPropertyChanged(nameof(RemoveConnections));
            }
        }

        private ICommand removeConnectionsCommand;

        public NodePinViewModel(NodePin pin)
        {
            this.Pin = pin;

            AllocatePinToInputCommand = new RelayCommand(AllocatePinToInputOutput) { CanExecute = true };
            RemoveConnectionsCommand = new RelayCommand(RemoveConnections) { CanExecute = true };
        }

        private void AllocatePinToInputOutput(object pinControl)
        {
            /*
             * The entire method is quite a crappy implementation and is a temporary work-around.
             * This should be rewritten/removed later when a better MVVM set-up for the application has been set up.
             */
            
            NodePin pin = (NodePin)pinControl;
            DependencyObject parentObject = VisualTreeHelper.GetParent(pin);

            // Find the node that contains the pin
            while (!(parentObject is UserControl))
            {
                parentObject = VisualTreeHelper.GetParent(parentObject);

                if (parentObject == null)
                    return;
            }

            // If this is a pin from a constant node then we can skip a lot of work
            var parentNodeAsConstantNode = parentObject as ConstantNode;

            if (parentNodeAsConstantNode != null)
            {
                this.parentNode = parentNodeAsConstantNode;

                var parentNodeViewModel = ((VisualConstantNodeViewModel) parentNodeAsConstantNode.DataContext);
                parentNodeViewModel.OutputPin.Pin = pin;
                this.DataType = parentNodeViewModel.OutputPin.DataType;

                return;
            }

            // Set the parent node in the pin's viewmodel
            this.ParentNode = (VisualNodeControl)parentObject;

            // Add this pin to the node's viewmodel input/output map
            // This will let us bind the pins to connectors later
            parentObject = VisualTreeHelper.GetParent(pin);

            int children = VisualTreeHelper.GetChildrenCount(parentObject);

            for (int i = 0; i < children; i++)
            {
                var child = VisualTreeHelper.GetChild(parentObject, i);

                Label pinLabel = child as Label;

                if (pinLabel == null)
                {
                    continue;
                }
                else if (!string.IsNullOrEmpty(pinLabel.Name))
                {
                    // Now we have a handle between the pin and its label
                    // We need to add the pin to its input/output field in the VisualNode viewmodel
                    VisualNodeViewModel parentNodeViewModel = (VisualNodeViewModel)this.ParentNode.DataContext;

                    if (pinLabel.Name == "InputLabel")
                    {
                        this.IsOutputPin = false;

                        foreach (PinModel input in parentNodeViewModel.Inputs)
                        {
                            if (input.Name == (string)pinLabel.Content)
                            {
                                input.Pin = pinControl as NodePin;
                                this.DataType = input.DataType;

                                break;
                            }
                        }
                    }
                    else if (pinLabel.Name == "OutputLabel")
                    {
                        this.IsOutputPin = true;

                        foreach (PinModel output in parentNodeViewModel.Outputs)
                        {
                            if (output.Name == (string)pinLabel.Content)
                            {
                                output.Pin = pinControl as NodePin;
                                this.DataType = output.DataType;

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void RemoveConnections()
        {
            GraphEditorViewModel editor = (GraphEditorViewModel)((MainWindow)(App.Current.MainWindow)).VisualEditor.DataContext;
            List<ConnectionViewModel> connectionsToRemove = new List<ConnectionViewModel>();

            foreach (var connection in editor.Connections)
            {
                if (connection.InputPin == this.Pin || connection.OutputPin == this.Pin)
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
