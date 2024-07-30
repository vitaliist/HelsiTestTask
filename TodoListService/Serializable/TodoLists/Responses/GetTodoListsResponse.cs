using TodoListService.Models;

namespace TodoListService.Serializable.TodoLists.Responses;

public class GetTodoListsResponse
{
    public List<TodoListShortModel> TodoLists { get; set; }
    public int? Page { get; set; }
}