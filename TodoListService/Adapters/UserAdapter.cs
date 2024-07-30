using TodoListService.Entities;
using TodoListService.Models;

namespace TodoListService.Adapters;

public static class UserAdapter
{
    public static User ToEntity(this UserModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        IsDeleted = model.IsDeleted
        
    };

    public static UserModel ToModel(this User entity) => new UserModel
    {
        Id = entity.Id,
        Name = entity.Name,
        IsDeleted = entity.IsDeleted
    };
}