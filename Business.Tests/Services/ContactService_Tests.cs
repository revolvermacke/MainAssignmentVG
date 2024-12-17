using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _contactService = new ContactService(_contactRepositoryMock.Object);
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


}
