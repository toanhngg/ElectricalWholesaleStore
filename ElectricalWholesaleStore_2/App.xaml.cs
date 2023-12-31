﻿using ElectricalWholesaleStore_2.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalWholesaleStore_2
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
            serviceColection.AddTransient<LoginWindow>();
         //   serviceColection.AddScoped<ElectricStoreContext>();
            ServiceProvider = serviceColection.BuildServiceProvider();
            ServiceProvider.GetRequiredService<LoginWindow>().Show();
        }
    }
}
