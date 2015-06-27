using Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualQueryApplication.ViewModels
{
    public class GraphEditorViewModel : ViewModelBase
    {
        public ObservableCollection<VisualNodeViewModel> VisualNodes
        {
            get
            {
                return visualNodes;
            }
        }

        private ObservableCollection<VisualNodeViewModel> visualNodes = new ObservableCollection<VisualNodeViewModel>();

        public List<GraphConnectionViewModel> Connections
        {
            get
            {
                return connections;
            }
            set
            {
                SetValue(ref connections, value);
            }
        }

        private List<GraphConnectionViewModel> connections = new List<GraphConnectionViewModel>();

        public GraphEditorViewModel()
        {
            visualNodes.Add(new VisualNodeViewModel(typeof(SubtractNode)));
            visualNodes.Add(new VisualNodeViewModel(typeof(AddNode)));
        }
    }
}
