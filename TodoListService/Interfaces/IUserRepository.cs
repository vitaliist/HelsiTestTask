using TodoListService.Entities;

namespace TodoListService.Interfaces;

public interface IUserRepository 
{
    Task CreateAsync(User entity, CancellationToken cToken = default);
    Task RemoveAsync(string id, CancellationToken cToken = default);
    Task<User?> GetUserAsync(string id, bool includeDeleted = false, CancellationToken cToken = default);
}