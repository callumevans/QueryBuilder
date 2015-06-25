using DataTypes;
using Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for VisualChartBox.xaml
    /// </summary>
    public partial class VisualGraphBox : UserControl
    {
        public VisualGraphBox(Type nodeType)
        {
            InitializeComponent();
            this.DataContext = new VisualGraphViewModel(nodeType);
        }

        public void RefreshInputs()
        {
            InputsPanel.Children.Clear();

            foreach (IDataTypeContainer input in ((VisualGraphViewModel)DataContext).Inputs)
            {
                InputsPanel.Children.Add(new NodePin());
            }
        }
    }
}
