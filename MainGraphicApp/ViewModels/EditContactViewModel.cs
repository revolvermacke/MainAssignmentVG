using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace MainGraphicApp.ViewModels;

public partial class EditContactViewModel(IContactService contactService, IServiceProvider serviceProvider) : ObservableObject
{
    private readonly IContactService _contactService = contactService;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [ObservableProperty]
    private Contact _contact = new();

    [RelayCommand]
    private void Save()
    {
        var result = _contactService.UpdateContact(Contact);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactViewModel>();
        }

    }

    [RelayCommand]
    private void GoToContacts()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactViewModel>();
    }
}
