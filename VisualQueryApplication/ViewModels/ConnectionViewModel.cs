using System;
using System.Collections.Generic;
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

        private GraphEditorViewModel graphViewModel;

        public ConnectionViewModel(GraphEditorViewModel graphViewModel, NodePin outputPin = null, NodePin inputPin = null)
        {
            this.graphViewModel = graphViewModel;
            this.OutputPin = outputPin;
            this.InputPin = inputPin;
        }
    }
}
