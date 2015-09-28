using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VisualQueryApplication.ViewModels
{
    public class DatabaseViewerViewModel : ViewModelBase
    {
        public DataTable DisplayedData
        {
            get { return displayedData; }
            set
            {
                displayedData = value;
                OnPropertyChanged(nameof(DisplayedData));
            }
        }

        private DataTable displayedData = new DataTable();

        public ObservableCollection<string> LoadedTables
        {
            get { return loadedTables; }
            set
            {
                loadedTables = value;
                OnPropertyChanged(nameof(LoadedTables));
            }
        }

        private ObservableCollection<string> loadedTables = new ObservableCollection<string>();

        public ICommand SelectedTableChangedCommand
        {
            get { return selectedTableChangedCommand; }
            set
            {
                selectedTableChangedCommand = value;
                OnPropertyChanged(nameof(SelectedTableChangedCommand));
            }
        }

        private ICommand selectedTableChangedCommand;

        public DatabaseViewerViewModel()
        {
            selectedTableChangedCommand = new RelayCommand(SelectedTableChanged);

            if (App.CurrentDatabaseConnection == null)
                return;

            SQLiteCommand sqlCommand = new SQLiteCommand(
                "SELECT * FROM sqlite_master WHERE type='table';",
                App.CurrentDatabaseConnection);

            SQLiteDataAdapter da = new SQLiteDataAdapter(sqlCommand);
            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                loadedTables.Add(dt.Rows[i]["name"].ToString());
            }
        }

        private void SelectedTableChanged(object tableName)
        {
            string table = tableName.ToString();
            SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + table + ";", App.CurrentDatabaseConnection);

            SQLiteDataAdapter da = new SQLiteDataAdapter(query);

            DataTable dt = new DataTable();
            da.Fill(dt);

            DisplayedData = dt;
        }
    }
}
