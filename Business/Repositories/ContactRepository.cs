using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Repositories;

public class ContactRepository(IFileService fileService) : IContactRepository
{
    private readonly IFileService _fileService = fileService;

    public List<Contact>? GetContacts()
    {
        try
        {
            var json = _fileService.GetContentFromFile();
            var contacts = JsonSerializer.Deserialize<List<Contact>>(json);

            return contacts;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }

    public bool SaveContacts(List<Contact> contacts)
    {
        try
        {
            var json = JsonSerializer.Serialize(contacts);
            _fileService.SaveContentToFile(json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
