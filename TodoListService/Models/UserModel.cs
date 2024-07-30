namespace TodoListService.Models;

public class UserModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    public bool IsDeleted { get; set; }
}