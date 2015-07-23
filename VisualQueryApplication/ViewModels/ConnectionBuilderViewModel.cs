using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VisualQueryApplication.Controls.GraphBuilder;

namespace VisualQueryApplication.ViewModels
{
    public class ConnectionBuilderViewModel : ViewModelBase
    {
        public NodePin OutputPin
        {
            get { return outputPin; }
            set
            {
                outputPin = value;
                OnPropertyChanged(nameof(OutputPin));
            }
        }

        private NodePin outputPin;

        public Point MousePosition
        {
            get { return mousePosition; }
            set
            {
                mousePosition = value;
                OnPropertyChanged(nameof(MousePosition));
            }
        }

        private Point mousePosition;

        public ConnectionBuilderViewModel()
        {
            OutputPin = null;
            MousePosition = new Point(0, 0);
        }
    }
}
