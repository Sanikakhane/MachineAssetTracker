using MachineAssetTracker.Models;
using MongoDB.Driver;

namespace MachineAssetTracker.Data
{
    public class MongoDBContext
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<MachineAsset> _machineAssets;
        public int count = 0;
        public MongoDBContext()
        {
            _mongoClient = new MongoClient("mongodb://localhost:30116/");
            _database = _mongoClient.GetDatabase("MachineAssestTracker");
            _machineAssets = _database.GetCollection<MachineAsset>("MachineAssest");
        }
        public void InsertMany(List<MachineAsset> machineAssets)
        {
            if (machineAssets.Count > 0)
            {
                foreach (var asset in machineAssets)
                {
                   
                    var existingAsset = _machineAssets
                        .Find(a => a.MachineType == asset.MachineType &&
                                   a.Asset == asset.Asset &&
                                   a.Series == asset.Series)
                        .FirstOrDefault();

                   
                    if (existingAsset == null)
                    {
                        _machineAssets.InsertOne(asset);
                    }
                }
            }
        }
        public List<MachineAsset> GetAll()
        {
            return _machineAssets.Find(_ => true).ToList();
        }
    }
}
