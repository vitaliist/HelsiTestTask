namespace TodoListService.Models;

public class TodoListModel
{
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public List<string> Tasks { get; set; } = new();
    public UserModel CreatedBy { get; set; } = null!;
    public HashSet<UserModel> SharedWith { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}