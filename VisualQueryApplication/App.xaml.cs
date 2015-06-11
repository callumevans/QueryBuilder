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
        public static string LayoutFile;

        public App()
        {
            LayoutFile = ConfigurationManager.AppSettings["DockLayoutFile"];

            if (!File.Exists(LayoutFile))
            {
                File.Create(LayoutFile);
                File.Copy(ConfigurationManager.AppSettings["DefaultDockLayoutFile"], LayoutFile);
            }
        }
    }
}
