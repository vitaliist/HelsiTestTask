using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoListService.Entities;

public class TodoList
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; }
    
    [BsonRequired]
    public string Name { get; init; } = null!;

    public List<string> Tasks { get; set; } = new();
    
    [BsonRequired]
    public User CreatedBy { get; init; } = null!;

    public HashSet<User> SharedWith { get; set; } = new();
    
    public DateTime CreatedAt { get; init; }
    
    public DateTime UpdatedAt { get; init; }
}

