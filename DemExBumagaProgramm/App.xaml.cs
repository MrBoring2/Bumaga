using DemExBumagaProgramm.Data;
using DemExBumagaProgramm.Views.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DemExBumagaProgramm
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BumagaContext Context { get; set; } = new BumagaContext();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var agentListWindow = new AgentsListWindow();
            agentListWindow.Show();
        }
    }
}
