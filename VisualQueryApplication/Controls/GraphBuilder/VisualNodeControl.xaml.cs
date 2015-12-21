using System.Windows.Controls;
using System.Windows.Input;
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication.Controls.GraphBuilder
{
    /// <summary>
    /// Interaction logic for VisualChartBox.xaml
    /// </summary>
    public partial class VisualNodeControl : UserControl
    {
        public VisualNodeControl()
        {
            InitializeComponent();
        }

        private void VisualNodeBoxControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VisualGraphComponentViewModel viewModel = ((VisualGraphComponentViewModel)this.DataContext);
            viewModel.ClickedCommand.Execute(null);
        }
    }
}
