using AdvancedDevSample.Application.DTOs.Users;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Entities;
using AdvancedDevSample.Domain.Interfaces.Users;

namespace AdvancedDevSample.Test.Application.Services;

public class UserServiceTests
{
    private readonly FakeUserRepository _fakeRepository;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _fakeRepository = new FakeUserRepository();
        _service = new UserService(_fakeRepository);
    }

    [Fact]
    public void GetAllUsers_Should_Return_Only_Active_Users()
    {

        var activeUser = new User(Guid.NewGuid(), "active", "active@test.com", "Active", "User");
        var inactiveUser = new User(Guid.NewGuid(), "inactive", "inactive@test.com", "Inactive", "User");
        inactiveUser.SetActive(false);

        _fakeRepository.Add(activeUser);
        _fakeRepository.Add(inactiveUser);


        var result = _service.GetAllUsers().ToList();


        Assert.Single(result);
        Assert.Equal("active", result[0].Username);
    }

    [Fact]
    public void GetUserById_Should_Return_User_When_Exists()
    {

        var user = new User(Guid.NewGuid(), "jdoe", "john@test.com", "John", "Doe");
        _fakeRepository.Add(user);


        var result = _service.GetUserById(user.Id);


        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal("jdoe", result.Username);
        Assert.Equal("John Doe", result.FullName);
    }

    [Fact]
    public void GetUserById_Should_Throw_Exception_When_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.GetUserById(Guid.NewGuid())));

        Assert.Equal("Utilisateur non trouvé", exception.Message);
    }

    [Fact]
    public void CreateUser_Should_Create_And_Return_User()
    {

        var request = new CreateUserRequest
        {
            Username = "newuser",
            Email = "new@user.com",
            FirstName = "New",
            LastName = "User",
            Role = "Admin"
        };


        var result = _service.CreateUser(request);


        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("newuser", result.Username);
        Assert.Equal("new@user.com", result.Email);
        Assert.Equal("New User", result.FullName);
        Assert.Equal("Admin", result.Role);
        Assert.True(result.IsActive);
    }

    [Fact]
    public void CreateUser_Should_Use_Default_Role_When_Not_Provided()
    {

        var request = new CreateUserRequest
        {
            Username = "newuser",
            Email = "new@user.com",
            FirstName = "New",
            LastName = "User",
            Role = "User"
        };


        var result = _service.CreateUser(request);


        Assert.Equal("User", result.Role);
    }

    [Fact]
    public void UpdateUser_Should_Update_All_Properties()
    {

        var user = new User(Guid.NewGuid(), "olduser", "old@email.com", "Old", "Name");
        _fakeRepository.Add(user);

        var request = new UpdateUserRequest
        {
            Username = "newuser",
            Email = "new@email.com",
            FirstName = "New",
            LastName = "Name"
        };


        var result = _service.UpdateUser(user.Id, request);


        Assert.Equal("newuser", result.Username);
        Assert.Equal("new@email.com", result.Email);
        Assert.Equal("New", result.FirstName);
        Assert.Equal("Name", result.LastName);
        Assert.Equal("New Name", result.FullName);
    }

    [Fact]
    public void UpdateUser_Should_Throw_Exception_When_Not_Found()
    {

        var request = new UpdateUserRequest
        {
            Username = "user",
            Email = "email@test.com",
            FirstName = "First",
            LastName = "Last"
        };


        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.UpdateUser(Guid.NewGuid(), request)));

        Assert.Equal("Utilisateur non trouvé", exception.Message);
    }

    [Fact]
    public void DeleteUser_Should_Set_User_Inactive()
    {

        var user = new User(Guid.NewGuid(), "testuser", "test@user.com", "Test", "User");
        _fakeRepository.Add(user);


        _service.DeleteUser(user.Id);


        var savedUser = _fakeRepository.GetByID(user.Id);
        Assert.NotNull(savedUser);
        Assert.False(savedUser.IsActive);
    }

    [Fact]
    public void DeleteUser_Should_Throw_Exception_When_Not_Found()
    {

        var exception = Assert.Throws<ApplicationServiceException>((Action)(() =>
            _service.DeleteUser(Guid.NewGuid())));

        Assert.Equal("Utilisateur non trouvé", exception.Message);
    }
}

/// <summary>
/// Fake Repository pour les tests (InMemory)
/// </summary>
public class FakeUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _users = new();

    public User? GetByID(Guid id)
    {
        _users.TryGetValue(id, out var user);
        return user;
    }

    public IEnumerable<User> GetAll()
    {
        return _users.Values.ToList();
    }

    public void Save(User user)
    {
        _users[user.Id] = user;
    }

    public void Add(User user)
    {
        _users[user.Id] = user;
    }
}
