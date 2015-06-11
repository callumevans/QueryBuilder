using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace VisualQueryApplication
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Toggleable Properties

        /// <summary>
        /// Whether or not the visual editor is showing
        /// </summary>
        public bool IsShowingVisualEditor
        {
            get
            {
                return isShowingVisualEditor;
            }
            set
            {
                isShowingVisualEditor = value;
                OnPropertyChanged("IsShowingVisualEditor");
            }
        }

        private bool isShowingVisualEditor;

        #endregion

        #region Commands

        /// <summary>
        /// Save the current layout
        /// </summary>
        public ICommand SaveLayoutCommand { get; set; }

        /// <summary>
        /// Load the most recently saved layout
        /// </summary>
        public ICommand LoadLayoutCommand { get; set; }

        #endregion

        public ViewModel()
        {
            SaveLayoutCommand = new RelayCommand(new Action<object>(SaveLayout));
            LoadLayoutCommand = new RelayCommand(new Action<object>(LoadLayout));
        }

        public void SaveLayout(object obj)
        {
            DockingManager dockingManager = (DockingManager)obj;
            XmlLayoutSerializer layoutSerialiser = new XmlLayoutSerializer(dockingManager);

            File.WriteAllText(App.LayoutFile, "");

            using (var writer = new StreamWriter(App.LayoutFile))
            {
                layoutSerialiser.Serialize(writer);
            }
        }

        public void LoadLayout(object obj)
        {
            DockingManager dockingManager = (DockingManager)obj;

            // Load layout
            try
            {
                XmlLayoutSerializer layoutSerialiser = new XmlLayoutSerializer(dockingManager);

                using (var reader = new StreamReader(App.LayoutFile))
                {
                    layoutSerialiser.Deserialize(reader);
                }
            }
            catch
            {
                // TO-DO: Ask user to reset layout file if it fails
            }
        }
    }
}
