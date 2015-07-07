using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualQueryApplication.Model;
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication.Controls.GraphBuilder
{
    /// <summary>
    /// Interaction logic for NodePin.xaml
    /// </summary>
    public partial class NodePin : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Point Centre
        {
            get
            {
                Point centre = this.TransformToAncestor(
                    ((MainWindow)App.Current.MainWindow).VisualEditor.ContentArea)
                    .Transform(new Point(this.Width / 2, this.Height / 2));

                return centre;
            }
        }

        public NodePin()
        {
            InitializeComponent();
        }

        public void ParentMoved()
        {
            OnPropertyChanged(nameof(Centre));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((NodePinViewModel)DataContext).AllocatePinToInputCommand.Execute(this);
        }
    }
}
