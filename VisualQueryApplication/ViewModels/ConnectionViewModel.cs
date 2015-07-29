using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VisualQueryApplication.Controls.GraphBuilder;

namespace VisualQueryApplication.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        public NodePin OutputPin { get; set; }
        public NodePin InputPin { get; set; }

        public Point CurvePoint1
        {
            get { return new Point(OutputPin.Centre.X + 60, OutputPin.Centre.Y); }
        }

        public Point CurvePoint2
        {
            get { return new Point(InputPin.Centre.X - 60, InputPin.Centre.Y); }
        }

        private GraphEditorViewModel graphViewModel;

        public ConnectionViewModel(GraphEditorViewModel graphViewModel, NodePin outputPin, NodePin inputPin)
        {
            this.graphViewModel = graphViewModel;
            this.OutputPin = outputPin;
            this.InputPin = inputPin;

            OutputPin.PropertyChanged += PinOnPropertyChanged;
            InputPin.PropertyChanged += PinOnPropertyChanged;
        }

        private void PinOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(nameof(CurvePoint1));
            OnPropertyChanged(nameof(CurvePoint2));
        }
    }
}
