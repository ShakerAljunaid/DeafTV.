using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Deaf_T.V__V1_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            if (e.Args != null && e.Args.Count() > 0)
            {
                mainWindow.StartupMovie = e.Args[0];
                mainWindow.Show();
            }
            else
            {
                mainWindow.Show();
            }
        }
        
    }
}
