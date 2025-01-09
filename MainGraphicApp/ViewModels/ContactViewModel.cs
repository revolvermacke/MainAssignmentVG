using Business.Interfaces;
using Business.Models;
using Business.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MainGraphicApp.ViewModels;

public partial class ContactViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    [ObservableProperty]
    private Contact _contact = new();

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    public ContactViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;

        _contacts = new ObservableCollection<Contact>(_contactService.GetAllContacts());
    }

    [RelayCommand]
    private void GoToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    private void GoToDetails(Contact contact)
    {
        var detailContactViewModel = _serviceProvider.GetRequiredService<DetailContactViewModel>();
        detailContactViewModel.Contact = contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = detailContactViewModel;
    }

    [RelayCommand]
    private void DeleteContact(Contact contact)
    {
        var result = _contactService.DeleteContact(contact.Id);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactViewModel>();
        }
    }
}
