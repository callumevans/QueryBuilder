using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualQueryApplication.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<string> loadedNodes;

        public List<string> LoadedNodes
        {
            get
            {

            }
            set
            {

            }
        }

        public MainWindowViewModel()
        {
            LoadedNod = new List<string>();
            LoadedNod.Add("Mu");
        }
    }
}
