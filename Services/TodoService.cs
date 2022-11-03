using Todos.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Todos.Services;

public class TodoService
{

    private readonly IMongoCollection<Todo> _todoCollection;

    public TodoService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _todoCollection = database.GetCollection<Todo>("Todo");
    }

    public async Task<Todo> GetAsync(string todoId)
    {
        return await _todoCollection.Find(todo => todo.Id == todoId).FirstAsync();
    }

    public async Task<List<Todo>> GetAllAsync(string userId)
    {
        return await _todoCollection.Find(todo => todo.userId == userId).ToListAsync();
    }
    public async Task CreateAsync(Todo todo)
    {
        await _todoCollection.InsertOneAsync(todo);
        return;
    }
    public async Task UpdateAsync(Todo todo)
    {
        UpdateDefinition<Todo> update = Builders<Todo>.Update.Set(
            field: "text",
            value: todo.text
        ).Set(
            field: "isDone",
            value: todo.isDone
        ).Set(
            field: "updateTime",
            value: DateTime.UtcNow
        );
        await _todoCollection.UpdateOneAsync(
            filter: doc => doc.Id == todo.Id,
            update: update
        );
        return;
    }
    public async Task DeleteAsync(string todoId)
    {
        await _todoCollection.DeleteOneAsync(todo => todo.Id == todoId);
        return;
    }
}