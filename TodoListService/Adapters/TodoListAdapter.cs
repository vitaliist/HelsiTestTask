using TodoListService.Entities;
using TodoListService.Models;

namespace TodoListService.Adapters;

public static class TodoListAdapter
{
    public static TodoList ToEntity(this TodoListModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        CreatedAt = model.CreatedAt,
        CreatedBy = model.CreatedBy.ToEntity(),
        SharedWith = model.SharedWith?.Select(x=>x.ToEntity()).ToHashSet() ?? new HashSet<User>(),
        Tasks = model.Tasks,
        UpdatedAt = model.UpdatedAt
    };

    public static TodoListModel ToModel(this TodoList entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        SharedWith = entity.SharedWith?.Select(x => x.ToModel()).ToHashSet() ?? new HashSet<UserModel>(),
        Tasks = entity.Tasks,
        CreatedBy = entity.CreatedBy.ToModel()
    };

    public static TodoListShortModel TodoListShortModel(this TodoList entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name
    };
}