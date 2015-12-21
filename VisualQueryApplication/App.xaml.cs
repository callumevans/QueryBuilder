using System;
using System.IO;
using System.Windows;

namespace VisualQueryApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string ApplicationRoot = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string PluginFolderPath = ApplicationRoot + "/NodePlugins";

        public App()
        {
            if (!Directory.Exists(PluginFolderPath))
                Directory.CreateDirectory(PluginFolderPath);
        }
    }
}
