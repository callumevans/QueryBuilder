using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualQueryApplication.ViewModels
{
    public class QueryResultsViewerViewModel : ViewModelBase
    {
        public DataTable QueryResult
        {
            get { return queryResult; }
            set
            {
                queryResult = value;
                OnPropertyChanged(nameof(QueryResult));
            }
        }

        private DataTable queryResult = new DataTable();

        public QueryResultsViewerViewModel()
        {
        }

        public QueryResultsViewerViewModel(string queryString)
        {
            if (App.CurrentDatabaseConnection == null)
                throw new Exception("Establish a database connection first.");

            SQLiteCommand query = new SQLiteCommand(queryString, App.CurrentDatabaseConnection);

            SQLiteDataAdapter da = new SQLiteDataAdapter(query);
            DataTable dt = new DataTable();

            da.Fill(dt);

            QueryResult = dt;
        }
    }
}
