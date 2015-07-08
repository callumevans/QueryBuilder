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

        public Type DataType
        {
            get
            {
                return dataType;
            }
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

        public SolidColorBrush PinColour
        {
            get
            {
                return pinColour;
            }
            set
            {
                pinColour = value;
                OnPropertyChanged("PinColour");
            }
        }

        private SolidColorBrush pinColour = new SolidColorBrush(Colors.Gray);

        public VisualNodeControl ParentNode
        {
            get
            {
                return parentNode;
            }
            set
            {
                parentNode = value;
                OnPropertyChanged("ParentNode");
            }
        }

        public VisualNodeControl parentNode;

        public ICommand AllocatePinToInputCommand
        {
            get
            {
                return allocatePinToInputCommand;
            }
            set
            {
                allocatePinToInputCommand = value;
                OnPropertyChanged("AllocatePinToInputCommand");
            }
        }

        private ICommand allocatePinToInputCommand;

        public NodePinViewModel()
        {
            AllocatePinToInputCommand = new RelayCommand(new Action<object>(AllocatePinToInput));
        }

        private void AllocatePinToInput(object pinControl)
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
    }
}
