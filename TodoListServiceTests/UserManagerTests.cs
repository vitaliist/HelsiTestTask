using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.AutoMock;
using TodoListService.Adapters;
using TodoListService.Entities;
using TodoListService.Interfaces;
using TodoListService.Managers;

namespace TodoListServiceTests;


[TestFixture]
public class UserManagerTests
{
    [Test]
    public async Task GetUserAsync_Success()
    {
        var container = BuildContainer();

        var user = new User { Id = new Random().Next().ToString(), Name = "Test"};
        var userModel = user.ToModel();
        container.Arrange<IUserRepository>(x => x.GetUserAsync(user.Id, true, CancellationToken.None)).Returns(Task.FromResult(user));
        var getUser = await container.Instance.GetUserAsync(user.Id, true, CancellationToken.None);
        Assert.That(userModel.Id, Is.EqualTo(getUser.Id));
    }

    private MockingContainer<UserManager> BuildContainer()
    {
        var mockingContainer = new MockingContainer<UserManager>();
        mockingContainer.Settings.MockBehavior = Behavior.Strict;
        return mockingContainer;
    }
}