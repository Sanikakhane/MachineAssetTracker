using MongoDB.Driver;

public abstract class MongoDBContextBase<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;
    private readonly MongoClient _mongoClient;
    protected readonly IMongoDatabase _database;

    public MongoDBContextBase(string collectionName)
    {
        _mongoClient = new MongoClient("mongodb://localhost:30116/"); // MongoDB connection
        _database = _mongoClient.GetDatabase("MachineAssetTracker");
        _collection = _database.GetCollection<T>(collectionName);
    }

    public abstract void InsertMany(List<T> data);
    public abstract List<T> GetAll();
}
