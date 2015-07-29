using Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataTypes;

namespace VisualQueryApplication.ViewModels
{
    public class GraphEditorViewModel : ViewModelBase
    {
        private readonly UserControl graphControl;

        public Point MousePoint
        {
            get
            {
                return new Point(
                    Mouse.GetPosition(graphControl).X,
                    Mouse.GetPosition(graphControl).Y);
            }
        }

        public ObservableCollection<VisualGraphComponentViewModel> VisualNodes
        {
            get { return visualNodes; }
            set
            {
                visualNodes = value;
                OnPropertyChanged(nameof(VisualNodes));
            }
        }

        private ObservableCollection<VisualGraphComponentViewModel> visualNodes = new ObservableCollection<VisualGraphComponentViewModel>();

        public ObservableCollection<ConnectionViewModel> Connections
        {
            get { return connections; }
            set
            {
                connections = value;
                OnPropertyChanged(nameof(Connections));
            }
        }

        private ObservableCollection<ConnectionViewModel> connections = new ObservableCollection<ConnectionViewModel>();

        public ICommand DeleteSelectedNodesCommand
        {
            get { return deleteSelectedNodesCommand; }
            set
            {
                deleteSelectedNodesCommand = value;
                OnPropertyChanged(nameof(DeleteSelectedNodesCommand));
            }
        }

        private ICommand deleteSelectedNodesCommand;

        public ICommand AddConstantCommand
        {
            get { return addConstantCommand; }
            set
            {
                addConstantCommand = value;
                OnPropertyChanged(nameof(AddConstantCommand));
            }
        }

        private ICommand addConstantCommand;

        public GraphEditorViewModel(UserControl graphControl)
        {
            this.graphControl = graphControl;

            DeleteSelectedNodesCommand = new RelayCommand(DeleteSelectedNodes) { CanExecute = true };
            AddConstantCommand = new RelayCommand(AddConstant) { CanExecute = true };
        }

        public int FindMaxZIndex()
        {
            int count = 0;

            if (visualNodes.Count > 0)
            {
                count = visualNodes[0].ZIndex;
            }
            else
            {
                return 0;
            }

            foreach (VisualGraphComponentViewModel node in visualNodes)
            {
                if (node.ZIndex > count)
                    count = node.ZIndex;
            }

            return count;
        }

        private void DeleteSelectedNodes()
        {
            List<VisualGraphComponentViewModel> nodesToDelete = new List<VisualGraphComponentViewModel>();

            foreach (VisualGraphComponentViewModel node in visualNodes)
            {
                if (node.IsSelected)
                    nodesToDelete.Add(node);
            }

            foreach (var node in nodesToDelete)
            {
                node.DeleteSelf.Execute(null);
            }
        }

        private void AddConstant(object param)
        {
            Type constantType = (Type)param;
            VisualNodes.Add(new VisualConstantNodeViewModel(constantType) { X = MousePoint.X, Y = MousePoint.Y });
        }
    }
}
