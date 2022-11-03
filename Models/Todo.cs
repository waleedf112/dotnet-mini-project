using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Todos.Models;

public class Todo
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string userId { get; set; } = null!;

    public string text { get; set; } = null!;

    public bool isDone { get; set; } = false;

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? creationTime { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? updateTime { get; set; } = DateTime.UtcNow;
}