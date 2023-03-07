namespace MongoExample.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

public class Dood
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] //Great way to work with it as a string, but it's actually a ObjectId in DB
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
}