using System.Windows.Controls;
using System.Windows.Input;
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication.Controls.GraphBuilder
{
    /// <summary>
    /// Interaction logic for ConstantNode.xaml
    /// </summary>
    public partial class ConstantNode : UserControl
    {
        public ConstantNode()
        {
            InitializeComponent();
        }

        private void ConstantNode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VisualGraphComponentViewModel viewModel = ((VisualGraphComponentViewModel)this.DataContext);
            viewModel.ClickedCommand.Execute(null);
        }
    }
}
