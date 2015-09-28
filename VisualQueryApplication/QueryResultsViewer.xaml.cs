using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VisualQueryApplication.ViewModels;

namespace VisualQueryApplication
{
    /// <summary>
    /// Interaction logic for QueryResultsViewer.xaml
    /// </summary>
    public partial class QueryResultsViewer : Window
    {
        private readonly Action CallbackOnClose;

        public QueryResultsViewer(Action parentCallbackOnClose, string queryString)
        {
            InitializeComponent();

            CallbackOnClose = parentCallbackOnClose;
            this.Closed += QueryViewer_Closed;

            this.DataContext = new QueryResultsViewerViewModel(queryString);
        }

        private void QueryViewer_Closed(object sender, EventArgs e)
        {
            CallbackOnClose.Invoke();
        }
    }
}
