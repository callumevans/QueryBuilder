using System;
using System.Collections.Generic;

namespace VisualQueryApplication.ViewModels
{
    public class VisualConstantNodeViewModel : VisualGraphComponentViewModel
    {
        public string Value { get; set; }

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
            this.OutputPin = new NodePinViewModel("Output", type, true, false, 0);
        }

        public override void RemoveConnections()
        {
            GraphEditorViewModel editor = (GraphEditorViewModel)((MainWindow)(App.Current.MainWindow)).VisualEditor.DataContext;
            
            List<ConnectionViewModel> connectionsToRemove = new List<ConnectionViewModel>();

            foreach (var connection in editor.Connections)
            {
                if (connection.OutputPinControl == this.OutputPin.Pin)
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
