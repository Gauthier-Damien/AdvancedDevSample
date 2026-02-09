using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Exceptions;

namespace AdvancedDevSample.Test.Domaine.Entities;

/// <summary>
/// Tests unitaires pour l'entité User
/// </summary>
public class UserTests
{
    [Fact]
    public void Constructor_Should_Create_User_With_Valid_Data()
    {

        var id = Guid.NewGuid();
        var username = "jdoe";
        var email = "john.doe@example.com";
        var firstName = "John";
        var lastName = "Doe";
        var role = "Admin";


        var user = new User(id, username, email, firstName, lastName, role);


        Assert.Equal(id, user.Id);
        Assert.Equal(username, user.Username);
        Assert.Equal(email, user.Email);
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.Equal(role, user.Role);
        Assert.True(user.IsActive);
        Assert.Equal("John Doe", user.FullName);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Username_Is_Empty()
    {

        var exception = Assert.Throws<DomainException>(() =>
            new User(Guid.NewGuid(), "", "email@test.com", "John", "Doe"));

        Assert.Equal("Le nom d'utilisateur est obligatoire.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Email_Is_Empty()
    {

        var exception = Assert.Throws<DomainException>(() =>
            new User(Guid.NewGuid(), "jdoe", "", "John", "Doe"));

        Assert.Equal("L'email est obligatoire.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_Email_Is_Invalid()
    {

        var exception = Assert.Throws<DomainException>(() =>
            new User(Guid.NewGuid(), "jdoe", "invalid-email", "John", "Doe"));

        Assert.Equal("L'email n'est pas valide.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_FirstName_Is_Empty()
    {

        var exception = Assert.Throws<DomainException>(() =>
            new User(Guid.NewGuid(), "jdoe", "email@test.com", "", "Doe"));

        Assert.Equal("Le prénom est obligatoire.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_When_LastName_Is_Empty()
    {

        var exception = Assert.Throws<DomainException>(() =>
            new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", ""));

        Assert.Equal("Le nom de famille est obligatoire.", exception.Message);
    }

    [Fact]
    public void Constructor_Should_Use_Default_Role_When_Not_Provided()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");


        Assert.Equal("User", user.Role);
    }

    [Fact]
    public void UpdateInfo_Should_Update_All_Properties()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "old@email.com", "John", "Doe");


        user.UpdateInfo("jdoe2", "new@email.com", "Jane", "Smith");


        Assert.Equal("jdoe2", user.Username);
        Assert.Equal("new@email.com", user.Email);
        Assert.Equal("Jane", user.FirstName);
        Assert.Equal("Smith", user.LastName);
        Assert.Equal("Jane Smith", user.FullName);
    }

    [Fact]
    public void UpdateInfo_Should_Throw_Exception_When_Username_Is_Invalid()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");


        var exception = Assert.Throws<DomainException>(() =>
            user.UpdateInfo("", "email@test.com", "John", "Doe"));

        Assert.Equal("Le nom d'utilisateur est obligatoire.", exception.Message);
    }

    [Fact]
    public void UpdateInfo_Should_Throw_Exception_When_Email_Is_Invalid()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");


        var exception = Assert.Throws<DomainException>(() =>
            user.UpdateInfo("jdoe", "invalid-email", "John", "Doe"));

        Assert.Equal("L'email n'est pas valide.", exception.Message);
    }

    [Fact]
    public void ChangeRole_Should_Update_Role()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");


        user.ChangeRole("Admin");


        Assert.Equal("Admin", user.Role);
    }

    [Fact]
    public void ChangeRole_Should_Throw_Exception_When_Role_Is_Empty()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");


        var exception = Assert.Throws<DomainException>(() =>
            user.ChangeRole(""));

        Assert.Equal("Le rôle est obligatoire.", exception.Message);
    }

    [Fact]
    public void SetActive_Should_Change_Status_To_False()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");
        Assert.True(user.IsActive);


        user.SetActive(false);


        Assert.False(user.IsActive);
    }

    [Fact]
    public void SetActive_Should_Change_Status_To_True()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");
        user.SetActive(false);


        user.SetActive(true);


        Assert.True(user.IsActive);
    }

    [Fact]
    public void DefaultConstructor_Should_Create_User_With_Default_Values()
    {

        var user = new User();


        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(string.Empty, user.Username);
        Assert.Equal(string.Empty, user.Email);
        Assert.Equal(string.Empty, user.FirstName);
        Assert.Equal(string.Empty, user.LastName);
        Assert.Equal("User", user.Role);
        Assert.True(user.IsActive);
    }

    [Fact]
    public void FullName_Should_Concatenate_FirstName_And_LastName()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");


        var fullName = user.FullName;


        Assert.Equal("John Doe", fullName);
    }

    [Fact]
    public void FullName_Should_Update_When_Names_Change()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "email@test.com", "John", "Doe");


        user.UpdateInfo("jdoe", "email@test.com", "Jane", "Smith");


        Assert.Equal("Jane Smith", user.FullName);
    }
}
