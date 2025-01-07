using MainGraphicApp.ViewModels;
using MainGraphicApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MainGraphicApp;


public partial class App : Application
{
    private IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ContactViewModel>();
                services.AddTransient<ContactsView>();

                services.AddTransient<AddContactViewModel>();
                services.AddTransient<AddContactView>();

                services.AddTransient<EditContactViewModel>();
                services.AddTransient<EditContactView>();

            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
