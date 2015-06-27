using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualQueryApplication.Controls.GraphBuilder;

namespace VisualQueryApplication.ViewModels
{
    public class GraphConnectionViewModel : ViewModelBase
    {
        public NodePin OutputPin
        {
            get
            {
                return outputPin;
            }
            set
            {
                SetValue(ref outputPin, value);
            }
        }

        private NodePin outputPin;

        public NodePin InputPin
        {
            get
            {
                return inputPin;
            }
            set
            {
                SetValue(ref inputPin, value);
            }
        }

        private NodePin inputPin;

        public GraphConnectionViewModel()
        {

        }

        public GraphConnectionViewModel(NodePin outputPin, NodePin inputPin)
        {
            this.outputPin = outputPin;
            this.inputPin = inputPin;
        }
    }
}
