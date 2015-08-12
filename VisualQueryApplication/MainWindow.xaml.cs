using Fluent;
using Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Common;
using Graph;
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
        private GeneratedQueryView queryViewWindow;

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

        private async void BuildQuery_Click(object sender, RoutedEventArgs e)
        {
            BuildButton.IsEnabled = false;

            NodeGraphManager builtGraph = new NodeGraphManager();
            builtGraph = await Graph.BuildGraphAsync(VisualEditor.DataContext as GraphEditorViewModel);

            ((MainWindowViewModel)this.DataContext).ActiveQueryState = builtGraph.QueryState;

            BuildButton.IsEnabled = true;
        }

        private void ViewQuery_Click(object sender, RoutedEventArgs e)
        {
            if (queryViewWindow != null)
            {
                queryViewWindow.Focus();
                return;
            }

            queryViewWindow = new GeneratedQueryView(
                new Action(() => queryViewWindow = null));

            var viewModel = ((MainWindowViewModel)this.DataContext);

            if (viewModel.ActiveQueryState.VariableBag.ContainsKey("Print Function"))
            {
                ((GeneratedQueryViewViewModel) queryViewWindow.DataContext).Query =
                    viewModel.ActiveQueryState.VariableBag["Print Function"].ToString();
            }

            queryViewWindow.Show();
        }
    }
}
