using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UPS.EmployeeManagement.Presentation.ViewModel;
using UPS.EmployeeManagement.Service;
using UPS.EmployeeManagement.Service.Impl;

namespace UPS.EmployeeManagement.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;
        private IRestClient restClient;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureRestClient();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureRestClient()
        {
            restClient = new RestClient();
            restClient.BaseUrl = new Uri("https://gorest.co.in/public-api/");
            restClient.Timeout = -1;
            restClient.AddDefaultHeader("Authorization", "Bearer fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
            restClient.AddDefaultHeader("Content-Type", "application/json");
        }

        private void ConfigureServices(IServiceCollection services) {
            services.AddSingleton(restClient);
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<EmployeeViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = serviceProvider.GetService<MainWindow>();
            MainWindow.DataContext = serviceProvider.GetService<MainViewModel>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
