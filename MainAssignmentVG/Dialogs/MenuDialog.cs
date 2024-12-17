using Business.Factories;
using Business.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace MainAssignmentVG.Dialogs;

public class MenuDialog(IContactService contactService)
{
    private readonly IContactService _contactService = contactService;


    public void Run()
    {
        while (true)
        {
            MainMenu();
        }
    }

    public void MainMenu()
    {
        Console.Clear();

        Console.WriteLine($"---- Choose an option -----\n");
        Console.WriteLine($"{"1.",-3} Create new contact.");
        Console.WriteLine($"{"2.",-3} View all contact.");
        Console.WriteLine($"{"Q.",-3} Exit application.\n");
        Console.WriteLine("---------------------------");

        var option = Console.ReadLine()!;

        switch (option.ToLower())
        {
            case "q":
                QuitApp();
                break;

            case "1":
                CreateContacts();
                break;

            case "2":
                ViewContacts();
                break;

            default:
                InvalidOption();
                break;
        }

    }

    public void QuitApp()
    {
        Console.Clear();
        Console.WriteLine("Are you sure you want to quit this application? (Yes/No)");
        var option = Console.ReadLine()!;

        if (option.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
        {
            Environment.Exit(0);
        }

    }
    public void CreateContacts()
    {
        var contacts = ContactFactory.Create();

        contacts.FirstName = Console.ReadLine()!;
        contacts.LastName = Console.ReadLine()!;
        contacts.Email = Console.ReadLine()!;
        contacts.Phone = Console.ReadLine()!;
        contacts.Address = Console.ReadLine()!;
        contacts.PostalCode = Console.ReadLine()!;
        contacts.City = Console.ReadLine()!;

        var result = _contactService.CreateContact(contacts);
        if (result)
            Console.WriteLine("Contact was created successfully!");
        else
            Console.WriteLine("Contact was NOT created successfully...");

        Console.ReadKey();

    }

    public void ViewContacts()
    {
        Console.Clear();

        foreach (var contact in _contactService.GetAllContacts())
        {
            Console.WriteLine($"{"Id: ",-3}{contact.Id}");
            Console.WriteLine($"{"Name: ",-3}{contact.FirstName} {contact.LastName}");
            Console.WriteLine($"{"E-mail: ",-3}{contact.Email}");
            Console.WriteLine($"{"Phone number: ",-3}{contact.Phone}");
            Console.WriteLine($"{"Address: ",-3}{contact.Address}");
            Console.WriteLine($"{"Postal code: ",-3}{contact.PostalCode}");
            Console.WriteLine($"{"City: ",-3}{contact.City}");
            Console.WriteLine("------------------------");
        }
    }

    public void InvalidOption()
    {
        Console.Clear();
        Console.WriteLine("You have to choose an option.");
        Console.ReadKey();
    }

}

