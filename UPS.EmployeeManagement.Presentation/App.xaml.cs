using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
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
        private IFlurlClient restClient;
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel
                .Debug()
                .WriteTo
                .RollingFile(Directory.GetCurrentDirectory() + "/Logs/log-{Date}.txt")
                .CreateLogger();

            var serviceCollection = new ServiceCollection();
            ConfigureRestClient();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureRestClient()
        {
            restClient = new FlurlClient("https://gorest.co.in/public-api/");
            restClient.WithHeader("Authorization", "Bearer fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
            restClient.WithHeader("Content-Type", "application/json");
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Add logging
            services.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder
                    .AddSerilog(dispose: true);
            }));

            services.AddLogging();
            services.AddSingleton(Log.Logger);
            services.AddSingleton(restClient);
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<EmployeeViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Information("OnStartup operations started...");
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //Configuration = builder.Build();

            MainWindow = serviceProvider.GetService<MainWindow>();
            MainWindow.DataContext = serviceProvider.GetService<MainViewModel>();
            MainWindow.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush();

            base.OnExit(e);
        }

    }
}
