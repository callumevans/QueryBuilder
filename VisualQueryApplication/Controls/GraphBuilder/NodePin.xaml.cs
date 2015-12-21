using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
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

                NodePinViewModel rootPin = (NodePinViewModel)connectionBuilder.OutputPin.DataContext;
                NodePinViewModel targetPin = (NodePinViewModel)this.DataContext;

                // Validate the output and input pins
                // Reverse them if needed
                // TODO: More extensive validation. ie. Check for output -> output or input -> input connections.

                // Ensure datatypes are the same between pins
                if (rootPin.DataType != targetPin.DataType)
                {
                    // If the data types do not match try and add in an auto-conversion
                    foreach (var rule in graph.ConversionRules)
                    {
                        if (rule.Item1 == rootPin.DataType && rule.Item2 == targetPin.DataType)
                        {
                            // Conversion rule found!
                            // Add the conversion node at the midpoint between pins
                            Point rootPoint = rootPin.Pin.Centre;
                            Point targetPoint = targetPin.Pin.Centre;

                            Point midPoint = new Point(
                                ((rootPoint.X + targetPoint.X) / 2),
                                 (rootPoint.Y + targetPoint.Y) / 2);

                            VisualNodeViewModel conversionNode = new VisualNodeViewModel(rule.Item3)
                            {
                                X = midPoint.X,
                                Y = midPoint.Y
                            };

                            graph.VisualNodes.Add(conversionNode);

                            // Generate the conversion node's view so we can access its pins
                            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

                            // Add the new connections, but ensure it is an OutputPin -> InputPin connection
                            // Otherwise, we need to correct for that here
                            if (((NodePinViewModel)connectionBuilder.OutputPin.DataContext).IsOutputPin == false)
                            {
                                // Connections need flipping
                                graph.Connections.Add(new ConnectionViewModel(targetPin.Pin, conversionNode.Inputs[0].Pin));
                                graph.Connections.Add(new ConnectionViewModel(conversionNode.Outputs[0].Pin, rootPin.Pin));
                            }
                            else
                            {
                                // Connection is correct
                                graph.Connections.Add(new ConnectionViewModel(rootPin.Pin, conversionNode.Inputs[0].Pin));
                                graph.Connections.Add(new ConnectionViewModel(conversionNode.Outputs[0].Pin, targetPin.Pin));
                            }

                        }
                    }
                }
                // If datatypes are the same, we can just create a connection between the pins
                // Make sure the connection is added in an OutputPin -> InputPin order
                else
                {
                    if (((NodePinViewModel)connectionBuilder.OutputPin.DataContext).IsOutputPin == false)
                        graph.Connections.Add(new ConnectionViewModel(this, connectionBuilder.OutputPin));
                    else
                        graph.Connections.Add(new ConnectionViewModel(connectionBuilder.OutputPin, this));
                }

                visualEditor.IsCreatingConnection = false;
            }
            else
            {
                // We need to initialise the 'create a connection' mode
                visualEditor.IsCreatingConnection = true;

                // And set the root pin
                ConnectionBuilderViewModel newConnection = ((ConnectionBuilderViewModel)visualEditor.NewConnectionLine.DataContext);
                newConnection.OutputPin = this;
            }
        }
    }
}
