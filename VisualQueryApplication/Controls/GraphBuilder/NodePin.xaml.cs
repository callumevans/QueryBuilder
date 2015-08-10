using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication.Controls.GraphBuilder
{
    /// <summary>
    /// Interaction logic for NodePin.xaml
    /// </summary>
    public partial class NodePin : UserControl, INotifyPropertyChanged
    {
        private SolidColorBrush pinColourCache;

        public event PropertyChangedEventHandler PropertyChanged;

        public Point Centre
        {
            get
            {
                Point centre = this.TransformToAncestor(
                    ((MainWindow)App.Current.MainWindow).VisualEditor.ContentArea)
                    .Transform(new Point(this.Width / 2, this.Height / 2));

                return centre;
            }
        }

        public NodePin()
        {
            InitializeComponent();
        }

        public void ParentMoved()
        {
            OnPropertyChanged(nameof(Centre));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NodePin_Loaded(object sender, RoutedEventArgs e)
        {
            NodePinViewModel viewModel = ((NodePinViewModel)DataContext);

            this.pinColourCache = viewModel.PinColour;
            viewModel.Pin = this;
        }


        private void NodePin_MouseEnter(object sender, MouseEventArgs e)
        {
            var colour = ((NodePinViewModel)DataContext).PinColour.Color;

            SolidColorBrush adjustedColour = new SolidColorBrush(Color.FromArgb(
                colour.A,
                (byte)Math.Min(255, colour.R + 255 * 0.3),
                (byte)Math.Min(255, colour.G + 255 * 0.3),
                (byte)Math.Min(255, colour.B + 255 * 0.3)
                ));

            ((NodePinViewModel)DataContext).PinColour = adjustedColour;
        }

        private void NodePin_MouseLeave(object sender, MouseEventArgs e)
        {
            ((NodePinViewModel)DataContext).PinColour = pinColourCache;
        }

        private void NodePin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(this);

            // Find the visual editor
            while (!(parentObject is VisualEditor))
            {
                parentObject = VisualTreeHelper.GetParent(parentObject);

                if (parentObject == null)
                    return;
            }

            VisualEditor visualEditor = parentObject as VisualEditor;

            // Determine how to handle the click
            if (visualEditor.IsCreatingConnection)
            {
                // If we are creating a new connection then we have to validate and add a new one
                GraphEditorViewModel graph = ((GraphEditorViewModel)visualEditor.DataContext);
                ConnectionBuilderViewModel connectionBuilder = ((ConnectionBuilderViewModel)visualEditor.NewConnectionLine.DataContext);

                // Validate the output and input pins
                // Reverse them if needed
                // TODO: More extensive validation. ie. Check for output -> output or input -> input connections.
                if (((NodePinViewModel)connectionBuilder.OutputPin.DataContext).IsOutputPin == false)
                    graph.Connections.Add(new ConnectionViewModel(this, connectionBuilder.OutputPin));
                else
                    graph.Connections.Add(new ConnectionViewModel(connectionBuilder.OutputPin, this));

                visualEditor.IsCreatingConnection = false;
            }
            else
            {
                // We need to initialise the 'create a connection' mode
                visualEditor.IsCreatingConnection = true;
            }

            ConnectionBuilderViewModel newConnection = ((ConnectionBuilderViewModel)visualEditor.NewConnectionLine.DataContext);

            newConnection.OutputPin = this;
        }
    }
}
