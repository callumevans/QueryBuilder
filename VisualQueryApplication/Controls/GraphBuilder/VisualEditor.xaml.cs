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
            NewConnectionLine.DataContext = new ConnectionBuilderViewModel();
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            VisualNodeViewModel node = (VisualNodeViewModel)thumb.DataContext;

            node.X += e.HorizontalChange;
            node.Y += e.VerticalChange;

            foreach (var input in node.Inputs)
            {
                input.Pin.ParentMoved();
            }

            foreach (var output in node.Outputs)
            {
                output.Pin.ParentMoved();
            }
        }

        private void Box_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserControl controlSender = (UserControl)sender;
            VisualNodeViewModel node = (VisualNodeViewModel)controlSender.DataContext;

            GraphEditorViewModel graphViewModel = (GraphEditorViewModel)DataContext;

            node.ZIndex = graphViewModel.FindMaxZIndex() + 10;
        }

        private void Editor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (VisualNodeViewModel node in ((GraphEditorViewModel)DataContext).VisualNodes)
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
    }
}
