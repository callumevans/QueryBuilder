using Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common;
using DataTypes;
using VisualQueryApplication.Controls.GraphBuilder;

namespace VisualQueryApplication.ViewModels
{
    public class GraphEditorViewModel : ViewModelBase
    {
        private readonly VisualEditor graphControl;

        public List<Tuple<Type, Type, Type>> ConversionRules
        {
            get
            {
                return new List<Tuple<Type, Type, Type>>(conversionRules);
            }
        }

        private List<Tuple<Type, Type, Type>> conversionRules = new List<Tuple<Type, Type, Type>>();

        public Point MousePoint
        {
            get
            {
                return new Point(
                    Mouse.GetPosition(graphControl).X + graphControl.pannerScroller.HorizontalOffset,
                    Mouse.GetPosition(graphControl).Y + graphControl.pannerScroller.VerticalOffset);
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

        public GraphEditorViewModel(VisualEditor graphControl)
        {
            this.graphControl = graphControl;

            DeleteSelectedNodesCommand = new RelayCommand(DeleteSelectedNodes) { CanExecute = true };
            AddConstantCommand = new RelayCommand(AddConstant) { CanExecute = true };
        }

        public void AddConversionRule(Type input, Type output, Type node)
        {
            // Input and output must be IDataTypes, node must be a NodeBase type
            if (typeof(IDataTypeContainer).IsAssignableFrom(input)  &&
                typeof(IDataTypeContainer).IsAssignableFrom(output) &&
                node.IsSubclassOf(typeof(NodeBase)))
            {
                conversionRules.Add(new Tuple<Type, Type, Type>(input, output, node));
            }
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
