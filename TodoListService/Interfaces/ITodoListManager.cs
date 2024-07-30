using TodoListService.Models;

namespace TodoListService.Interfaces;

public interface ITodoListManager
{
    Task CreateAsync(TodoListModel todoList, CancellationToken cToken = default);
    Task<TodoListModel?> GetTodoListAsync(string id, string userId, CancellationToken cToken = default);
    Task<(List<TodoListShortModel> lists, int? pageNumber)> GetTodoListsAsync(string userId, int? pageNumber, int itemsPerPage, CancellationToken cToken = default);
    Task UpdateTodoListAsync(TodoListModel updatedList, string userId, CancellationToken cToken = default);
    Task RemoveAsync(string id, string userId, CancellationToken cToken = default);
    Task AddUserToShareListAsync(string listId, string requestedByUserId, UserModel user, CancellationToken cToken);
    Task RemoveUserFromShareListAsync(string listId, string requestedByUserId, string targetUserId, CancellationToken cToken);
}