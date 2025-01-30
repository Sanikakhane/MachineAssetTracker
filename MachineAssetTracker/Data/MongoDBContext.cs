using MachineAssetTracker.Models;
using MongoDB.Driver;

namespace MachineAssetTracker.Data
{
    public class MongoDBContext
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<Machine> _machines;
        private IMongoCollection<Asset> _assets;
        private IMongoCollection<MachineAsset> _machineAssets;

        public MongoDBContext()
        {
            _mongoClient = new MongoClient("mongodb://localhost:30116/"); // MongoDB connection
            _database = _mongoClient.GetDatabase("MachineAssetTracker"); // Database name
            _machineAssets = _database.GetCollection<MachineAsset>("MachineAsset");
            _machines = _database.GetCollection<Machine>("Machines");
            _assets = _database.GetCollection<Asset>("Assets");
        }

        public void InsertMachines(List<Machine> machines)
        {
            if (machines.Count > 0)
            {
                foreach (Machine machine in machines)
                {
                    var existingMachine = _machines
                        .Find(m => m.MachineType == machine.MachineType)
                        .FirstOrDefault();

                    if (existingMachine == null)
                    {
                        _machines.InsertOne(machine);
                    }
                }
            }
        }

        public void InsertAssets(List<Asset> assets)
        {
            if (assets.Count > 0)
            {
                foreach (Asset asset in assets)
                {
                    var existingAsset = _assets
                        .Find(a => a.AssetName == asset.AssetName)
                        .FirstOrDefault();

                    if (existingAsset == null)
                    {
                        _assets.InsertOne(asset);
                    }
                }
            }
        }

        public void InsertMachineAssets(List<MachineAsset> machineAssets)
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

        public List<MachineAsset> GetAllMachineAssets()
        {
            return _machineAssets.Find(_ => true).ToList();
        }
        public List<Machine> GetAllMachines()
        {
            return _machines.Find(_ => true).ToList();
        }
        public List<Asset> GetAllAssets()
        {
            return _assets.Find(_ => true).ToList();
        }
    }

}

