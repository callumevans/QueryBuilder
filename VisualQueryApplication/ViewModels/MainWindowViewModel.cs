using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace VisualQueryApplication.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<Type> LoadedNodes { get; private set; } = new List<Type>();
        public QueryState ActiveQueryState { get; set; } = new QueryState();

        private GraphEditorViewModel graphViewModel;

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

        public MainWindowViewModel(GraphEditorViewModel graphViewModel)
        {
            this.graphViewModel = graphViewModel;

            InsertNodeCommand = new RelayCommand(InsertNode) { CanExecute = true };

            // Load in usable nodes
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
                            // Get unique name
                            LoadedNodes.Add(type);

                            // If this is a conversion node tell our graph about it
                            ConversionRule conversionRule = type.GetCustomAttribute(typeof(ConversionRule)) as ConversionRule;

                            if (conversionRule != null)
                            {
                                graphViewModel.AddConversionRule(conversionRule.InputType, conversionRule.OutputType, type);
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Error loading nodes.\nPlease ensure there are libraries in the /NodePlugins folder and that they are unblocked.");
                }
            }
        }

        private void InsertNode(object type)
        {
            Type nodeType = type as Type;

            if (type != null)
                graphViewModel.VisualNodes.Add(new VisualNodeViewModel(nodeType));
        }
    }
}
