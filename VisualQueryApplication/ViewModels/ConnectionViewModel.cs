using System.ComponentModel;
using System.Windows;
using VisualQueryApplication.Controls.GraphBuilder;

namespace VisualQueryApplication.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        public NodePinViewModel OutputPin { get; private set; }
        public NodePinViewModel InputPin { get; private set; }

        public NodePin OutputPinControl
        {
            get { return outputPinControl; }
            set
            {
                outputPinControl = value;
                OutputPin = (NodePinViewModel)outputPinControl.DataContext;
            }
        }

        private NodePin outputPinControl;

        public NodePin InputPinControl
        {
            get { return inputPinControl; }
            set
            {
                inputPinControl = value;
                InputPin = (NodePinViewModel)inputPinControl.DataContext;
            }
        }

        private NodePin inputPinControl;

        public Point CurvePoint1
        {
            get { return new Point(OutputPinControl.Centre.X + 60, OutputPinControl.Centre.Y); }
        }

        public Point CurvePoint2
        {
            get { return new Point(InputPinControl.Centre.X - 60, InputPinControl.Centre.Y); }
        }

        public ConnectionViewModel(NodePin outputPinControl, NodePin inputPinControl)
        {
            this.OutputPinControl = outputPinControl;
            this.InputPinControl = inputPinControl;

            OutputPinControl.PropertyChanged += PinOnPropertyChanged;
            InputPinControl.PropertyChanged += PinOnPropertyChanged;
        }

        private void PinOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(nameof(CurvePoint1));
            OnPropertyChanged(nameof(CurvePoint2));
        }
    }
}
