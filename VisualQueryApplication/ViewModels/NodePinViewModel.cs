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

namespace VisualQueryApplication.ViewModels
{
    public class NodePinViewModel : ViewModelBase
    {
        public NodePin Pin { get; set; }
        public int Index { get; private set; }
        public bool IsOutputPin { get; set; }
        public bool IsExecutionPin { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string name;

        public Type DataType
        {
            get { return dataType; }
            set
            {
                dataType = value;
                OnPropertyChanged(nameof(DataType));

                // Update pin colour
                if (value == typeof(DataTypes.Numeric))
                    PinColour = new SolidColorBrush(Colors.Green);
                else if (value == typeof(DataTypes.Boolean))
                    PinColour = new SolidColorBrush(Colors.DarkRed);
                else if (value == typeof(DataTypes.String))
                    PinColour = new SolidColorBrush(Colors.HotPink);
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

        public NodePinViewModel(string name, Type dataType, bool isOutputPin, bool isExecutionPin, int index)
        {
            this.Name = name;
            this.DataType = dataType;
            this.IsOutputPin = isOutputPin;
            this.IsExecutionPin = isExecutionPin;
            this.Index = index;

            RemoveConnectionsCommand = new RelayCommand(RemoveConnections) { CanExecute = true };
        }

        private void RemoveConnections()
        {
            GraphEditorViewModel editor = (GraphEditorViewModel)((MainWindow)(App.Current.MainWindow)).VisualEditor.DataContext;
            List<ConnectionViewModel> connectionsToRemove = new List<ConnectionViewModel>();

            foreach (var connection in editor.Connections)
            {
                if (connection.InputPinControl == this.Pin || connection.OutputPinControl == this.Pin)
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
