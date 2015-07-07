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
        public ItemsControl ContentArea
        {
            get
            {
                return ContentDisplay;
            }
        }

        public VisualEditor()
        {
            InitializeComponent();
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
    }
}
