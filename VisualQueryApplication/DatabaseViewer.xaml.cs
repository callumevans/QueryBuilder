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

namespace VisualQueryApplication
{
    /// <summary>
    /// Interaction logic for DatabaseViewer.xaml
    /// </summary>
    public partial class DatabaseViewer : Window
    {
        private readonly Action CallbackOnClose;

        private List<string> loadedTables;

        public DatabaseViewer(Action parentCallbackOnClose)
        {
            InitializeComponent();

            CallbackOnClose = parentCallbackOnClose;
            this.Closed += DatabaseViewer_Closed;

            loadedTables = new List<string>();

            // TODO: MAKE MVVM!!
            var conn = App.CurrentDatabaseConnection;

            SQLiteCommand sqlCommand = new SQLiteCommand(
                "SELECT * FROM sqlite_master WHERE type='table';",
                conn);

            SQLiteDataAdapter da = new SQLiteDataAdapter(sqlCommand);
            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                treeView.Items.Add(dt.Rows[i]["name"].ToString());
            }
        }

        private void DatabaseViewer_Closed(object sender, EventArgs e)
        {
            CallbackOnClose.Invoke();
        }
    }
}
