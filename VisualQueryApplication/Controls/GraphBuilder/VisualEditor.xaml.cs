using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Graph;
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

        public void ConstructGraph()
        {
            GraphEditorViewModel viewModel = ((GraphEditorViewModel) this.DataContext);

            // Start by getting the first executable node with no input
            VisualNodeViewModel startNode;

            foreach (var node in viewModel.VisualNodes)
            {
                var visualNode = node as VisualNodeViewModel;
                if (visualNode != null)
                {
                    if (visualNode.ExecutionInputs.Count == 1)
                    {
                        bool hasConnection = false;

                        // If the node has an execution input we need to see if it's connected
                        // If it isn't then we can start with that
                        foreach (var connection in viewModel.Connections)
                        {

                            NodePinViewModel connectedPin = (NodePinViewModel)connection.OutputPin.DataContext;
                            if (connectedPin == visualNode.ExecutionInputs[0])
                            {
                                hasConnection = true;
                            }
                        }

                        // Input pin has no connections
                        if (!hasConnection)
                        {
                            MessageBox.Show(visualNode.NodeTitle);
                            break;
                        }
                    }
                }
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
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
            }

            VisualConstantNodeViewModel constantNode = node as VisualConstantNodeViewModel;

            if (constantNode != null)
            {
                constantNode.OutputPin.Pin.ParentMoved();
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
            connectionLine.MousePosition = new Point(e.GetPosition(this).X - 1, e.GetPosition(this).Y - 1);
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
    }
}
