using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication.Controls.GraphBuilder
{
    /// <summary>
    /// Interaction logic for VisualEditor.xaml
    /// </summary>
    public partial class VisualEditor : UserControl
    {
        public bool IsCreatingConnection
        {
            get { return isCreatingConnection; }
            set
            {
                isCreatingConnection = value;

                if (IsCreatingConnection)
                    NewConnectionLine.Visibility = Visibility.Visible;
                else
                    NewConnectionLine.Visibility = Visibility.Hidden;
            }
        }

        private bool isCreatingConnection = false;

        public ItemsControl ContentArea
        {
            get { return ContentDisplay; }
        }

        public VisualEditor()
        {
            InitializeComponent();

            this.DataContext = new GraphEditorViewModel(this);
            NewConnectionLine.DataContext = new ConnectionBuilderViewModel();
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (IsCreatingConnection)
                return;

            Thumb thumb = (Thumb)sender;

            var node = (VisualGraphComponentViewModel)thumb.DataContext;

            node.X += e.HorizontalChange;
            node.Y += e.VerticalChange;

            VisualNodeViewModel visualNode = node as VisualNodeViewModel;

            if (visualNode != null)
            {
                foreach (var input in visualNode.Inputs)
                {
                    input.Pin.ParentMoved();
                }

                foreach (var output in visualNode.Outputs)
                {
                    output.Pin.ParentMoved();
                }

                foreach (var executionInput in visualNode.ExecutionInputs)
                {
                    executionInput.Pin.ParentMoved();
                }

                foreach (var executionOutput in visualNode.ExecutionOutputs)
                {
                    executionOutput.Pin.ParentMoved();
                }

                return;
            }

            VisualConstantNodeViewModel constantNode = node as VisualConstantNodeViewModel;

            if (constantNode != null)
            {
                constantNode.OutputPin.Pin.ParentMoved();
                return;
            }
        }

        private void VisualNode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserControl controlSender = (UserControl)sender;
            VisualGraphComponentViewModel node = (VisualGraphComponentViewModel)controlSender.DataContext;

            GraphEditorViewModel graphViewModel = (GraphEditorViewModel)DataContext;

            node.ZIndex = graphViewModel.FindMaxZIndex() + 10;
        }

        private void Editor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // If we are creating a connection...
            if (IsCreatingConnection)
            {
                ConnectionBuilderViewModel connectionBuilder = (ConnectionBuilderViewModel)NewConnectionLine.DataContext;
                NodePinViewModel connectionRootPin = (NodePinViewModel)connectionBuilder.OutputPin.DataContext;

                // Check if we are clicking from an input pin
                // If we are, try to automatically place a constant node connecting to the input pin
                if (connectionRootPin.IsOutputPin == false && connectionRootPin.IsExecutionPin == false)
                {
                    GraphEditorViewModel viewModel = (GraphEditorViewModel)this.DataContext;
                    VisualConstantNodeViewModel autoConstantNode = new VisualConstantNodeViewModel(connectionRootPin.DataType)
                    {
                        X = viewModel.MousePoint.X,
                        Y = viewModel.MousePoint.Y
                    };

                    viewModel.VisualNodes.Add(autoConstantNode);

                    // Generates the view for the NodePin in the constant node before we add it
                    Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

                    viewModel.Connections.Add(new ConnectionViewModel(autoConstantNode.OutputPin.Pin, connectionRootPin.Pin));
                }

                IsCreatingConnection = false;
                return;
            }

            // If we aren't creating a connection just deselect any nodes
            foreach (VisualGraphComponentViewModel node in ((GraphEditorViewModel)DataContext).VisualNodes)
            {
                node.IsSelected = false;
            }
        }

        private void VisualEditor_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!isCreatingConnection)
                return;

            var connectionLine = ((ConnectionBuilderViewModel)NewConnectionLine.DataContext);
            connectionLine.MousePosition = ((GraphEditorViewModel)this.DataContext).MousePoint;
        }

        private void VisualEditor_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:

                    if (IsCreatingConnection)
                    {
                        IsCreatingConnection = false;
                        return;
                    }

                    ((GraphEditorViewModel)DataContext).DeleteSelectedNodesCommand.Execute(null);
                    break;
            }
        }

        private void VisualEditor_OnLostFocus(object sender, RoutedEventArgs e)
        {
            foreach (var node in ((GraphEditorViewModel)DataContext).VisualNodes)
            {
                node.IsSelected = false;
            }
        }
    }
}
