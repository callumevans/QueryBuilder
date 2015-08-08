using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Win32;
using MessageBox = System.Windows.MessageBox;

namespace VisualQueryApplication.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<Type> loadedNodeTypes = new List<Type>();
        private readonly GraphEditorViewModel graphViewModel;

        public QueryState ActiveQueryState { get; set; } = new QueryState();

        /// <summary>
        /// List of nodes that are loaded in the system
        /// </summary>
        public ObservableCollection<string> LoadedNodes
        {
            get { return loadedNodes; }
            private set
            {
                loadedNodes = value;
                OnPropertyChanged(nameof(LoadedNodes));
            }
        }

        private ObservableCollection<string> loadedNodes = new ObservableCollection<string>();

        public ICommand LoadNodesCommand
        {
            get { return loadNodesCommand; }
            set
            {
                loadNodesCommand = value;
                OnPropertyChanged(nameof(LoadNodesCommand));
            }
        }

        private ICommand loadNodesCommand;

        public ICommand InsertNodeCommand
        {
            get { return insertNodeCommand; }
            set
            {
                insertNodeCommand = value;
                OnPropertyChanged(nameof(InsertNodeCommand));
            }
        }

        private ICommand insertNodeCommand;

        public ICommand LoadDatabaseCommand
        {
            get { return loadDatabaseCommand; }
            set
            {
                loadDatabaseCommand = value;
                OnPropertyChanged(nameof(LoadDatabaseCommand));
            }
        }

        private ICommand loadDatabaseCommand;

        public ICommand SaveQueryCommand
        {
            get { return saveQueryCommand; }
            set
            {
                saveQueryCommand = value;
                OnPropertyChanged(nameof(SaveQueryCommand));
            }
        }

        private ICommand saveQueryCommand;

        public ICommand LoadQueryCommand
        {
            get { return loadQueryCommand; }
            set
            {
                loadQueryCommand = value;
                OnPropertyChanged(nameof(LoadQueryCommand));
            }
        }

        private ICommand loadQueryCommand;

        public MainWindowViewModel()
        {
            LoadNodesCommand = new RelayCommand(LoadNodes) { CanExecute = true };
            InsertNodeCommand = new RelayCommand(InsertNode) { CanExecute = true };
            LoadDatabaseCommand = new RelayCommand(LoadDatabaseFile) { CanExecute = true };
            SaveQueryCommand = new RelayCommand(SaveQuery) { CanExecute = true };
            LoadQueryCommand = new RelayCommand(LoadQuery) { CanExecute = true };
        }

        public MainWindowViewModel(GraphEditorViewModel graphViewModel = null)
        {
            this.graphViewModel = graphViewModel;

            LoadNodesCommand = new RelayCommand(LoadNodes) { CanExecute = true };
            InsertNodeCommand = new RelayCommand(InsertNode) { CanExecute = true };
            LoadDatabaseCommand = new RelayCommand(LoadDatabaseFile) { CanExecute = true };
        }

        private void InsertNode(object selectedIndex)
        {
            if ((int)selectedIndex == -1)
                return;

            Type nodeToLoad = loadedNodeTypes[(int)selectedIndex];
            graphViewModel.VisualNodes.Add(new VisualNodeViewModel(nodeToLoad));
        }

        private void LoadNodes()
        {
            this.loadedNodeTypes.Clear();
            this.LoadedNodes.Clear();

            string[] dllFile = Directory.GetFiles(App.PluginFolderPath, "*.dll");
            List<Assembly> assemblies = new List<Assembly>(dllFile.Length);

            foreach (string file in dllFile)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    Type[] types = assembly.GetTypes();

                    // Look for NodeBase types
                    foreach (Type type in types)
                    {
                        if (type.IsSubclassOf(typeof(NodeBase)))
                        {
                            loadedNodeTypes.Add(type);
                            LoadedNodes.Add(GetNodeTypeName(type));
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Error loading nodes.");
                }
            }
        }

        private void SaveQuery()
        {
            var fileDialog = new System.Windows.Forms.SaveFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = File.Open(fileDialog.FileName, FileMode.Create))
                {
                    GraphEditorViewModel graph = ((MainWindow)App.Current.MainWindow).VisualEditor.DataContext as GraphEditorViewModel;

                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, graph);
                }
            }
        }

        private void LoadQuery()
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = File.Open(fileDialog.FileName, FileMode.Open))
                {
                    var binaryFormatter = new BinaryFormatter();
                    var loadedGraph = (GraphEditorViewModel) binaryFormatter.Deserialize(stream);

                    ((MainWindow)App.Current.MainWindow).VisualEditor.DataContext = loadedGraph;
                }
            }
        }

        private string GetNodeTypeName(Type node)
        {
            object attribute = node.GetCustomAttribute(typeof(NodeName));

            if (attribute != null)
                return ((NodeName)attribute).Name;

            return null;
        }

        private void LoadDatabaseFile()
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (fileDialog.FileName != null)
                    App.NewDatabaseFromQuery(fileDialog.FileName);
            }
        }
    }
}
