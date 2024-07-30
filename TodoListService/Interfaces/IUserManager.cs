using TodoListService.Models;

namespace TodoListService.Interfaces;

public interface IUserManager
{
    Task<UserModel?> GetUserAsync(string userId, bool includeDeleted = false, CancellationToken cToken = default);
    Task CreateUserAsync(UserModel model, CancellationToken cancellationToken = default);
    Task RemoveUserAsync(string userId, CancellationToken cancellationToken = default);
}