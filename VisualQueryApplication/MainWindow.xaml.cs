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

            VisualEditor.ContentArea.Children.Add(new VisualGraphBox(typeof(AddNode)) { Margin = new Thickness(20, 10, 20, 20)});
            VisualEditor.ContentArea.Children.Add(new VisualGraphBox(typeof(SubtractNode)) { Margin = new Thickness(230, 30, 20, 20) });
            VisualEditor.ContentArea.Children.Add(new VisualGraphBox(typeof(PrintNode)) { Margin = new Thickness(430, 60, 20, 20) });
        }
    }
}
