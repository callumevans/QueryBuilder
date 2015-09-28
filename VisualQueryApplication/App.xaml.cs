using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VisualQueryApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SQLiteConnection CurrentDatabaseConnection
        {
            get { return currentDatabaseConnection; }
        }

        private static SQLiteConnection currentDatabaseConnection;

        public static readonly string ApplicationRoot = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string PluginFolderPath = ApplicationRoot + "/NodePlugins";

        public App()
        {
            if (!Directory.Exists(PluginFolderPath))
                Directory.CreateDirectory(PluginFolderPath);
        }

        public static void NewDatabaseFromQuery(string queryFile)
        {
            currentDatabaseConnection = new SQLiteConnection();

            SQLiteCommand sqlCommand = new SQLiteCommand(
                File.ReadAllText(queryFile),
                currentDatabaseConnection);

            currentDatabaseConnection.ConnectionString = "DataSource=:memory:;version=3;";
            currentDatabaseConnection.Open();

            // Populate dataset
            SQLiteDataAdapter DB = new SQLiteDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            DB.Fill(ds);
        }
    }
}
