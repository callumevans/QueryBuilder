using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        public static readonly string applicationRoot = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string pluginFolderPath = applicationRoot + "/NodePlugins";

        public App()
        {
            if (!Directory.Exists(pluginFolderPath))
                Directory.CreateDirectory(pluginFolderPath);
        }
    }
}
