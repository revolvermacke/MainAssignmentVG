using Business.Interfaces;
using Business.Repositories;
using Business.Services;
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
                services.AddSingleton<IFileService>(new FileService(AppDomain.CurrentDomain.BaseDirectory, "contacts.json"));
                services.AddSingleton<IContactRepository, ContactRepository>();
                services.AddSingleton<IContactService, ContactService>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ContactViewModel>();
                services.AddTransient<ContactsView>();

                services.AddTransient<AddContactViewModel>();
                services.AddTransient<AddContactView>();

                services.AddTransient<EditContactViewModel>();
                services.AddTransient<EditContactView>();

                services.AddTransient<DetailContactViewModel>();
                services.AddTransient<DetailContactView>();

            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ContactViewModel>();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
