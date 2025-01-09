using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository, IFileService fileService) : IContactService
{
    private readonly IFileService _fileService = fileService;
    private readonly IContactRepository _contactRepository = contactRepository;
    private List<Contact> _contacts = [];


    public bool CreateContact(Contact contact)
    {
        contact.Id = UniqueIDGenerator.GenerateUniqueId();

        _contacts.Add(contact);

        var result = _contactRepository.SaveContacts(_contacts);
        return result;
    }

    public IEnumerable<Contact> GetAllContacts()
    {
        _contacts = _contactRepository.GetContacts()!;
        return _contacts;
    }

    public bool UpdateContact(Contact contact)
    {
        try
        {
            if (contact == null)
            {
                Debug.WriteLine("Contact cannot be null");
                return false;
            }
            var contactList = _contactRepository.GetContacts() ?? [];
            var contactUpdate = contactList.FirstOrDefault(c => c.Id == contact.Id);

            if (contactUpdate == null)
            {
                Debug.WriteLine("Contact was not found");
                return false;
            }
            contactUpdate.FirstName = contact.FirstName;
            contactUpdate.LastName = contact.LastName;
            contactUpdate.Email = contact.Email;
            contactUpdate.Phone = contact.Phone;
            contactUpdate.Address = contact.Address;
            contactUpdate.PostalCode = contact.PostalCode;
            contactUpdate.City = contact.City;

            _contactRepository.SaveContacts(contactList);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeleteContact(string id)
    {
        // Hitta kontakten med det angivna ID:t
        var contact = _contacts.FirstOrDefault(c => c.Id == id);

        if (contact != null)
        {
            // Ta bort kontakten från listan
            _contacts.Remove(contact);
            var result = _contactRepository.SaveContacts(_contacts);
            return true; // Indikerar att borttagningen lyckades
        }

        return false; // Indikerar att kontakten inte hittades
    }



}
