using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        }
    }
}
