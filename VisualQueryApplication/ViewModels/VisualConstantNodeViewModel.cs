using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualQueryApplication.Controls.GraphBuilder;

namespace VisualQueryApplication.ViewModels
{
    public class VisualConstantNodeViewModel : VisualGraphComponentViewModel
    {
        public NodePinViewModel OutputPin
        {
            get { return outputPin; }
            set
            {
                outputPin = value;
                OnPropertyChanged(nameof(OutputPin));
            }
        }

        private NodePinViewModel outputPin;

        public VisualConstantNodeViewModel(Type type)
        {
            this.OutputPin = new NodePinViewModel("Output", type, true, false);
        }

        public override void RemoveConnections()
        {
            GraphEditorViewModel editor = (GraphEditorViewModel)((MainWindow)(App.Current.MainWindow)).VisualEditor.DataContext;
            
            List<ConnectionViewModel> connectionsToRemove = new List<ConnectionViewModel>();

            foreach (var connection in editor.Connections)
            {
                if (connection.OutputPin == this.OutputPin.Pin)
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
