using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisualQueryApplication.Model;

namespace VisualQueryApplication.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        MainWindowModel model = new MainWindowModel();

        /// <summary>
        /// List of nodes that are loaded in the system
        /// </summary>
        public ObservableCollection<string> LoadedNodes
        {
            get
            {
                return loadedNodes;
            }
            private set
            {
                SetValue(ref loadedNodes, value);
            }
        }

        private ObservableCollection<string> loadedNodes = new ObservableCollection<string>();

        public ICommand LoadNodesCommand
        {
            get
            {
                return new RelayCommand(LoadNodes);
            }
        }

        public MainWindowViewModel()
        {
        }

        private void LoadNodes()
        {
            model.LoadedNodes.Clear();
            this.LoadedNodes.Clear();

            string[] dllFile = Directory.GetFiles(App.pluginFolderPath, "*.dll");
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
                            model.LoadedNodes.Add(type);
                            LoadedNodes.Add(GetNodeTypeName(type));
                        }
                    }
                }
                catch
                {
                    throw new Exception("Error loading nodes.");
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
    }
}
