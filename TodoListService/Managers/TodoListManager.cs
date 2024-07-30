using TodoListService.Adapters;
using TodoListService.Interfaces;
using TodoListService.Models;

namespace TodoListService.Managers;

public class TodoListManager : ITodoListManager
{
    private readonly ITodoListRepository _repository;

    public TodoListManager(ITodoListRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(TodoListModel todoList, CancellationToken cToken = default)
    {
        todoList.CreatedAt = DateTime.UtcNow;
        todoList.UpdatedAt = DateTime.UtcNow;
        
        todoList.Tasks = new List<string>();
        await _repository.CreateAsync(todoList.ToEntity(), cToken);
    }

    public async Task<TodoListModel?> GetTodoListAsync(string id, string userId, CancellationToken cToken = default)
    {
        var todoList = await _repository.GetTodoListAsync(id, userId, cToken);
        return todoList?.ToModel();
    }
    
    public async Task<(List<TodoListShortModel> lists, int? pageNumber)> GetTodoListsAsync(string userId, int? pageNumber, int itemsPerPage, CancellationToken cToken = default)
    {
        var todoLists = await _repository.GetTodoListsAsync(userId, pageNumber, itemsPerPage, cToken);
        var lists = todoLists.Select(x => x.TodoListShortModel()).ToList();
        pageNumber = pageNumber == null ? 1 : pageNumber+1;
        return (lists, pageNumber);
    }

    public async Task UpdateTodoListAsync(TodoListModel updatedList, string userId, CancellationToken cToken = default)
    {
        await _repository.UpdateTodoListAsync(updatedList.ToEntity(), userId, cToken);
    }
    
    public async Task RemoveAsync(string id, string userId, CancellationToken cToken = default)
    {
        await _repository.RemoveAsync(id, userId, cToken);
    }

    public async Task AddUserToShareListAsync(string listId, string requestedByUserId, UserModel user, CancellationToken cToken)
    {
        await _repository.AddUserToShareListAsync(listId, requestedByUserId, user.ToEntity(), cToken);
    }

    public async Task RemoveUserFromShareListAsync(string listId, string requestedByUserId, string targetUserId, CancellationToken cToken)
    {
        await _repository.RemoveUserFromShareListAsync(listId, requestedByUserId, targetUserId, cToken);
    }
}