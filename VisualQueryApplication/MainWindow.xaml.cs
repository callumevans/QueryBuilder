using Fluent;
using Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private QueryResultsViewer queryResultsViewWindow;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel(VisualEditor.DataContext as GraphEditorViewModel);
        }

        private void applicationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = this.DataContext as MainWindowViewModel;

            Dictionary<TreeViewItem, string> nodeTreeItems = new Dictionary<TreeViewItem, string>();
            Dictionary<string, TreeViewItem> rootCategories = new Dictionary<string, TreeViewItem>();

            // Load in all nodes
            foreach (var node in viewModel.LoadedNodes)
            {
                NodeCategory categoryAttribute = (NodeCategory)node.GetCustomAttribute(typeof(NodeCategory));
                NodeName nameAttribute = (NodeName)node.GetCustomAttribute(typeof(NodeName));

                if (categoryAttribute != null)
                    nodeTreeItems.Add(new TreeViewItem() { Header = nameAttribute.Name, Tag = node, FontWeight = FontWeights.Normal }, categoryAttribute.Category);
                else
                    nodeTreeItems.Add(new TreeViewItem() { Header = nameAttribute.Name, Tag = node, FontWeight = FontWeights.Normal }, "Uncategorised");
            }

            // Extract categories
            foreach (var node in nodeTreeItems)
            {
                if (!rootCategories.ContainsKey(node.Value))
                    rootCategories.Add(node.Value, new TreeViewItem() { Header = node.Value, FontWeight = FontWeights.Bold, IsExpanded = true });
            }

            // Display categories
            foreach (var category in rootCategories)
            {
                SelectableNodesTree.Items.Add(category.Value);
            }

            // Display nodes by category
            foreach (var node in nodeTreeItems)
            {
                if (node.Value == "Uncategorised")
                {
                    foreach (var categoryItem in rootCategories.Where(
                        categoryItem => categoryItem.Key.Equals("Uncategorised")))
                    {
                        categoryItem.Value.Items.Add(node.Key);
                    }
                }
                else
                {
                    foreach (var categoryItem in rootCategories.Where(
                        categoryItem => categoryItem.Key.Equals(node.Value)))
                    {
                        categoryItem.Value.Items.Add(node.Key);
                    }
                }
            }
        }

        private void LoadedNodesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = SelectableNodesTree.SelectedItem as TreeViewItem;

            if (selectedItem != null && selectedItem.Tag != null)
                ((MainWindowViewModel)DataContext).InsertNodeCommand.Execute(selectedItem.Tag);
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

            try
            {
                NodeGraphManager builtGraph = new NodeGraphManager();
                builtGraph = await Graph.BuildGraphAsync(VisualEditor.DataContext as GraphEditorViewModel);

                ((MainWindowViewModel) this.DataContext).ActiveQueryState = builtGraph.QueryState;
            }
            catch
            {
                MessageBox.Show("Error building graph.\n" + e.ToString());
            }
            finally
            {
                BuildButton.IsEnabled = true;
            }
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

            if (viewModel.ActiveQueryState != null)
            {
                ((GeneratedQueryViewViewModel) queryViewWindow.DataContext).Query =
                    Graph.BuildQuery(viewModel.ActiveQueryState);
            }

            queryViewWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            GraphEditorViewModel graph = (GraphEditorViewModel) VisualEditor.DataContext;
            graph.VisualNodes.Clear();
            graph.Connections.Clear();
        }

        private void RunQuery_OnClick(object sender, RoutedEventArgs e)
        {
            if (queryResultsViewWindow != null)
            {
                queryResultsViewWindow.Focus();
                return;
            }

            var viewModel = ((MainWindowViewModel)this.DataContext);

            if (viewModel.ActiveQueryState != null)
            {
                queryResultsViewWindow = new QueryResultsViewer(
                    new Action(() => queryResultsViewWindow = null),
                    Graph.BuildQuery(viewModel.ActiveQueryState));

                queryResultsViewWindow.Show();
            }
            else
            {
                MessageBox.Show("Please create and build an SQL query first.");
            }
        }
    }
}
