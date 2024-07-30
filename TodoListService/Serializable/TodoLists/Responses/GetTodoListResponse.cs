using Microsoft.AspNetCore.Mvc;
using TodoListService.Models;

namespace TodoListService.Serializable.TodoLists.Responses;

public class GetTodoListResponse
{
    public TodoListModel? TodoList { get; set; }
}