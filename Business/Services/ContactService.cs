using Business.Helpers;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
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
}
