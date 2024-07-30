using MongoDB.Driver;
using TodoListService.Entities;
using TodoListService.Interfaces;

namespace TodoListService.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly IMongoCollection<TodoList> _collection;
    
    public TodoListRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TodoList>("todolists");
    }
    public async Task<TodoList?> GetTodoListAsync(string listId, string userId, CancellationToken cToken = default)
    {
        var filter = BuildFilter(listId, userId);

        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken: cToken);
    }

    public async Task<List<TodoList>> GetTodoListsAsync(string userId, int? pageNumber, int itemsPerPage, CancellationToken cToken = default)
    {
        return await _collection.Find(x=> x.CreatedBy.Id == userId || x.SharedWith.Any(y=>y.Id == userId))
            .SortByDescending(o=>o.CreatedAt).Skip(pageNumber == null ? 0: (pageNumber-1) * itemsPerPage)
            .Limit(itemsPerPage)
            .ToListAsync(cancellationToken: cToken);
    }
    
    public async Task CreateAsync(TodoList entity, CancellationToken cToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken:cToken);
    }
    
    public async Task UpdateTodoListAsync(TodoList updatedList, string userId, CancellationToken cToken = default)
    {
        var filter = BuildFilter(updatedList.Id, userId);
        
        var update = Builders<TodoList>.Update
            .Set(x => x.Name, updatedList.Name)
            .Set(x => x.Tasks, updatedList.Tasks)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cToken);
    }


    public async Task AddUserToShareListAsync(string listId, string requestedByUserId, User user, CancellationToken cToken)
    {
        var filter = BuildFilter(listId, requestedByUserId);
        var update = Builders<TodoList>.Update.AddToSet(x => x.SharedWith, user);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cToken);
    }
    
    public async Task RemoveUserFromShareListAsync(string listId, string requestedByUserId, string targetUserId, CancellationToken cToken)
    {
        var filter = BuildFilter(listId, requestedByUserId);

        var update = Builders<TodoList>.Update.PullFilter(x => x.SharedWith, y=> y.Id == targetUserId);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cToken);
    }
    
    public async Task RemoveAsync(string listId, string userId, CancellationToken cToken = default)
    {
        var builder = Builders<TodoList>.Filter;
        var filter = builder.Eq(x=>x.Id, listId) & (builder.Eq(x => x.CreatedBy.Id, userId));
        await _collection.DeleteOneAsync(filter, cancellationToken: cToken);
    }

    private FilterDefinition<TodoList> BuildFilter(string todoListId, string userId)
    {
        var builder = Builders<TodoList>.Filter;
        var filter = builder.Eq(x=>x.Id, todoListId) &
                     (builder.Eq(x => x.CreatedBy.Id, userId) |
                      builder.ElemMatch(x => x.SharedWith, x => x.Id == userId));
        return filter;
    }
}