using Fluent;
using Nodes;
using System;
using System.Collections.Generic;
using System.IO;
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
using VisualQueryApplication.Controls.GraphBuilder;
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private GraphEditorViewModel vm;

        private void applicationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            vm = (GraphEditorViewModel)this.VisualEditor.DataContext;
            VisualNodeViewModel addNode = new VisualNodeViewModel(typeof(AddNode));
            VisualNodeViewModel subNode = new VisualNodeViewModel(typeof(SubtractNode));

            vm.VisualNodes.Add(addNode);
            vm.VisualNodes.Add(subNode);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.Connections.Add(new ConnectionViewModel(vm,
                vm.VisualNodes[0].Outputs[0].Pin,
                vm.VisualNodes[1].Inputs[0].Pin));
        }
    }
}
