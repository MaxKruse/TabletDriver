using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VMulti_Installer_GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            string s = null;
            if (e.Args.Length > 0)
                s = e.Args[0];
            switch (s.TrimStart('-'))
            {
                case "i":
                case "install":
                    window.InstallVMulti(sender, new RoutedEventArgs());
                    break;
                case "u":
                case "uninstall":
                    window.UninstallVMulti(sender, new RoutedEventArgs());
                    break;
                default:
                    break;
            }
        }
    }
}
