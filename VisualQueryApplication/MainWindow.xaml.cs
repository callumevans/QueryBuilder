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
        private DatabaseViewer databaseViewWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void applicationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MainWindowViewModel((GraphEditorViewModel)VisualEditor.DataContext);
            ((MainWindowViewModel)DataContext).LoadNodesCommand.Execute(null);
        }

        private void LoadedNodesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindowViewModel)DataContext).InsertNodeCommand.Execute(LoadedNodesList.SelectedIndex);
        }

        private void ViewDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (databaseViewWindow != null)
            {
                databaseViewWindow.Focus();
                return;
            }

            databaseViewWindow = new DatabaseViewer(
                new Action(() => databaseViewWindow = null));

            databaseViewWindow.Show();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            VisualEditor.ConstructGraph();
        }
    }
}
