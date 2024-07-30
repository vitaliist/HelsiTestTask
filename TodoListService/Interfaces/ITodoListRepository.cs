using TodoListService.Entities;

namespace TodoListService.Interfaces;

public interface ITodoListRepository
{
    Task CreateAsync(TodoList todoList, CancellationToken cToken);
    Task<TodoList?> GetTodoListAsync(string id, string userId, CancellationToken cToken = default);
    Task<List<TodoList>> GetTodoListsAsync(string userId, int? pageNumber, int itemsPerPage, CancellationToken cToken = default);
    Task UpdateTodoListAsync(TodoList updatedList, string userId, CancellationToken cToken = default);
    Task RemoveAsync(string id, string userId, CancellationToken cToken = default);
    Task AddUserToShareListAsync(string listId, string requestedByUserId, User user, CancellationToken cToken);
    Task RemoveUserFromShareListAsync(string listId, string requestedByUserId, string userId, CancellationToken cToken);
}