using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoListService.Entities;


public class User
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string  Id { get; set; }
    
    [BsonRequired]
    public string Name { get; set; }
    
    public bool IsDeleted { get; set; }
}