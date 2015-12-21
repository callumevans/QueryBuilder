using System.Windows.Input;

namespace VisualQueryApplication.ViewModels
{
    public abstract class VisualGraphComponentViewModel : ViewModelBase
    {
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        protected bool isSelected = false;

        public int ZIndex
        {
            get { return zIndex; }
            set
            {
                zIndex = value;
                OnPropertyChanged(nameof(ZIndex));
            }
        }

        protected int zIndex = 0;

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        protected double x = 20;

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        protected double y = 20;

        public ICommand DeleteSelf
        {
            get { return deleteSelf; }
            set
            {
                deleteSelf = value;
                OnPropertyChanged(nameof(DeleteSelf));
            }
        }

        private ICommand deleteSelf;

        public ICommand ClickedCommand
        {
            get { return clickedCommand; }
            set
            {
                clickedCommand = value;
                OnPropertyChanged(nameof(ClickedCommand));
            }
        }

        private ICommand clickedCommand;

        public VisualGraphComponentViewModel()
        {
            DeleteSelf = new RelayCommand(Delete) { CanExecute = true };
            ClickedCommand = new RelayCommand(Clicked) { CanExecute = true };
        }

        protected void Delete()
        {
            GraphEditorViewModel graphViewModel = ((MainWindow)App.Current.MainWindow)
                .VisualEditor
                .DataContext as GraphEditorViewModel;

            if (graphViewModel.VisualNodes.Contains(this))
            {
                this.RemoveConnections();
                graphViewModel.VisualNodes.Remove(this);
            }
        }

        protected void Clicked()
        {
            GraphEditorViewModel editor = (GraphEditorViewModel)((MainWindow)(App.Current.MainWindow)).VisualEditor.DataContext;

            foreach (VisualGraphComponentViewModel visualNode in editor.VisualNodes)
            {
                visualNode.IsSelected = false;
            }

            IsSelected = true;
        }

        public abstract void RemoveConnections();
    }
}
