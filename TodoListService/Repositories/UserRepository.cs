using MongoDB.Driver;
using TodoListService.Entities;
using TodoListService.Interfaces;

namespace TodoListService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<User>("users");
    }
    
    public async Task<User?> GetUserAsync(string id, bool includeDeleted = false, CancellationToken cToken = default)
    {
        var builder = Builders<User>.Filter;

        if (!includeDeleted)
            return await _collection.Find(builder.Eq(x=>x.Id, id)).FirstOrDefaultAsync(cancellationToken: cToken);

        var filter = builder.Eq(x => x.Id, id) & builder.Eq("IsDeleted", true);

        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken: cToken);
    }

    public async Task CreateAsync(User entity, CancellationToken cToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cToken);
    }
    
    public async Task RemoveAsync(string id, CancellationToken cToken = default)
    {
        var filter = Builders<User>.Filter.Eq(x => x.Id, id);
        var update = Builders<User>.Update.Set(x => x.IsDeleted, true);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cToken);
    }
}