using Microsoft.AspNetCore.Mvc;

namespace TodoListService.Serializable.TodoLists;

public record GetTodoListRequest([FromRoute] string Id, [FromQuery] string UserId);
public record GetTodoListsForUserRequest(string RequestedByUserId, int? Page, int ItemsPerPage = 25);
public record CreateOrUpdateTodoList(string? Id, string Name, string RequestedByUserId, List<string> Tasks);
public record ShareTodoListRequest(string TodoListId, string RequestedByUserId, string TargetUserId, bool IsRemove = false);
public record DeleteTodoListRequest([FromRoute] string Id, [FromQuery] string RequestedByUserId);
