using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class DoodRepository
{
    private readonly IMongoCollection<Dood> _doodCollection;

    public DoodRepository()
    {
        MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("DBURL"));
        IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DBNAME"));
        _doodCollection = database.GetCollection<Dood>("Projekat");
    }

    public async Task CreateAsync(Dood dood)
    {
        await _doodCollection.InsertOneAsync(dood);
        return;
    }

    public async Task<List<Dood>> GetAsync()
    {
        return await _doodCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToList(string id, string doodId)
    {
        FilterDefinition<Dood> filter = Builders<Dood>.Filter.Eq("Id", id);
        UpdateDefinition<Dood> update = Builders<Dood>.Update.AddToSet<string>("doodId", doodId);
        await _doodCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Dood> filter = Builders<Dood>.Filter.Eq("Id", id);
        await _doodCollection.DeleteOneAsync(filter);
        return;
    }
}