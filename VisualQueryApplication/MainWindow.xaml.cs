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

        private void applicationWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    ((GraphEditorViewModel)VisualEditor.DataContext).DeleteSelectedNodesCommand.Execute(null);
                    break;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: REMOVE
            var dc = ((GraphEditorViewModel)VisualEditor.DataContext);
            dc.VisualNodes.Add(new VisualNodeViewModel(typeof(AddNode)));
            dc.VisualNodes.Add(new VisualNodeViewModel(typeof(AddNode)));
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            var dc = ((GraphEditorViewModel)VisualEditor.DataContext);

            // TODO: REMOVE
            dc.Connections.Add(new ConnectionViewModel(dc)
            {
                InputPin = dc.VisualNodes[0].Outputs[0].Pin,
                OutputPin = dc.VisualNodes[1].Inputs[0].Pin
            });
        }
    }
}
