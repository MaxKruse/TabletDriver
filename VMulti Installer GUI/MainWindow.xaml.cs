using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using VMulti_Installer;
using Version = VMulti_Installer.Version;

namespace VMulti_Installer_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Log("Operating System Architecture: " + Version);
        }

        public Version Version => Environment.Is64BitOperatingSystem ? Version.x64 : Version.x86;

        private void InstallVMulti(object sender, RoutedEventArgs e)
        {
            var installer = new Installer();
            installer.LogUpdated += LogEvent;
            var thread = new Thread(() => installer.Install(Version))
            {
                Name = "VMulti_Install",
                IsBackground = true,
            };
            thread.Start();
            while (thread.ThreadState == ThreadState.Running)
                ButtonPanel.IsEnabled = false;
            ButtonPanel.IsEnabled = true;
        }

        private void UninstallVMulti(object sender, RoutedEventArgs e)
        {
            var installer = new Installer();
            installer.LogUpdated += LogEvent;
            var thread = new Thread(() => installer.Uninstall(Version))
            {
                Name = "VMulti_Uninstall",
                IsBackground = true,
            };
            thread.Start();
            while (thread.ThreadState == ThreadState.Running)
                ButtonPanel.IsEnabled = false;
            ButtonPanel.IsEnabled = true;
        }

        private void LogEvent(object sender, string e) => Dispatcher.Invoke(() => Log(e));

        public void Log(string line)
        {
            if (!string.IsNullOrWhiteSpace(OutputTextbox.Text))
                OutputTextbox.Text += Environment.NewLine;
            OutputTextbox.Text += line;
        }

        private void CopyAllConsole(object sender, RoutedEventArgs e) => Clipboard.SetText(OutputTextbox.Text);
        private void ClearAllConsole(object sender, RoutedEventArgs e) => OutputTextbox.Text = string.Empty;
    }
}
