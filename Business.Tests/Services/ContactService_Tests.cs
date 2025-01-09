using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;


namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _fileServiceMock = new Mock<IFileService>();
        _contactService = new ContactService(_contactRepositoryMock.Object, _fileServiceMock.Object);
    }

    [Fact]
    public void CreateContact_ShouldReturnTrue_WhenSuccess()
    {
        // arrange
        var contact = new Contact();
        _contactRepositoryMock.Setup(cr => cr.SaveContacts(It.IsAny<List<Contact>>())).Returns(true);


        // act
        var result = _contactService.CreateContact(contact);


        // assert
        Assert.True(result);
    }

    [Fact]
    public void UpdateContact_ShouldUpdateContact_WhenContactExists()
    {
        // Arrange
        var contact = new Contact
        {
            Id = "123",
            FirstName = "Updated",
            LastName = "Name",
            Email = "updated@example.com",
            Phone = "123456789",
            Address = "Updated Address",
            PostalCode = "12345",
            City = "Updated City"
        };

        var existingContact = new Contact
        {
            Id = "123",
            FirstName = "Old",
            LastName = "Name",
            Email = "old@example.com",
            Phone = "987654321",
            Address = "Old Address",
            PostalCode = "54321",
            City = "Old City"
        };

        var contactList = new List<Contact> {existingContact};

        // Mock GetContacts to return a list containing the existing contact
        _contactRepositoryMock
            .Setup(cr => cr.GetContacts())
            .Returns(contactList);

        // Act
        var result = _contactService.UpdateContact(contact);

        // Assert
        Assert.True(result);
        Assert.Equal(contact.FirstName, existingContact.FirstName);
        Assert.Equal(contact.LastName, existingContact.LastName);
        Assert.Equal(contact.Email, existingContact.Email);
        Assert.Equal(contact.Phone, existingContact.Phone);
        Assert.Equal(contact.Address, existingContact.Address);
        Assert.Equal(contact.PostalCode, existingContact.PostalCode);
        Assert.Equal(contact.City, existingContact.City);

        // Verify that SaveContacts was called once
        _contactRepositoryMock.Verify(repo => repo.SaveContacts(contactList), Times.Once);
    }

    [Fact]
    public void DeleteContact_ShouldReturnTrue_WhenContactExists()
    {
        // Arrange
        var contact = new Contact { Id = "123" };
        var contactsList = new List<Contact> { contact };

        _contactRepositoryMock
            .Setup(cr => cr.GetContacts())
            .Returns(contactsList);

        _contactService.GetAllContacts();

        _contactRepositoryMock
            .Setup(cr => cr.SaveContacts(It.IsAny<List<Contact>>()))
            .Returns(true);

        // Act
        var result = _contactService.DeleteContact("123");

        // Assert
        Assert.True(result);
        Assert.DoesNotContain(contact, contactsList);
        _contactRepositoryMock.Verify(repo => repo.SaveContacts(contactsList), Times.Once);
    }


}
