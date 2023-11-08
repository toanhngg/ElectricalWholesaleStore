using ElecStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ElecStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }
        public IConfiguration Configuration { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceColection = new ServiceCollection();
            serviceColection.AddTransient<MainWindow>();
            serviceColection.AddScoped<ElecStore.Models.ElectricStoreContext>();
            ServiceProvider = serviceColection.BuildServiceProvider();
            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }
    }
}
