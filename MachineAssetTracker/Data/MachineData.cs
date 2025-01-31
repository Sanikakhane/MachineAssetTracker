using MachineAssetTracker.Models;
using MongoDB.Driver;

namespace MachineAssetTracker.Data
{
    public class MachineData : MongoDBContextBase<Machine>
    {
        

        private readonly IMongoCollection<Asset> _assetCollection;
        private readonly IMongoCollection<MachineAsset> _machineAssetCollection;

        public MachineData() : base("Machines")
        {
            _assetCollection = _database.GetCollection<Asset>("Assets");
            _machineAssetCollection = _database.GetCollection<MachineAsset>("MachineAssets");
        }

        
        public override List<Machine> GetAll()
        {
            return _collection.Find(machine => true).ToList();
        }

    
        public void InsertMachineWithAssets(Machine machine, List<Asset> assets)
        {
            var existingMachine = _collection.Find(a => a.Id == machine.Id).FirstOrDefault();
            if (existingMachine == null)
            {
                _collection.InsertOne(machine);
            }
            

            foreach (var asset in assets)
            {
                var existingAsset = _assetCollection.Find(a => a.AssetName == asset.AssetName).FirstOrDefault();
                if (existingAsset == null)
                {
                    _assetCollection.InsertOne(asset);
                }
                else
                {
                    foreach (var series in asset.Series)
                    {
                        if (!existingAsset.Series.Contains(series))
                        {
                            existingAsset.Series.Add(series);
                        }
                        _assetCollection.ReplaceOne(a => a.AssetName == asset.AssetName, existingAsset);
                    }
                }

            }

            foreach (var asset in machine.Assets)
            {
                foreach(var series in asset.Series)
                {
                    var existingMachineAsset = _machineAssetCollection.Find(ma => ma.Id == machine.Id && ma.Asset == asset.AssetName && ma.Series == series).FirstOrDefault();
                    if(existingMachineAsset == null)
                    {
                        var machineAsset = new MachineAsset
                        {
                            Id = machine.Id,
                            MachineType = machine.MachineType,
                            Asset = asset.AssetName,
                            Series = series
                        };
                        _machineAssetCollection.InsertOne(machineAsset);
                    }

                    
                }
            }
        }

  
        public void UpdateMachine(string Id,Machine machine)
        {
            var existingMachine = _collection.Find(m => m.Id == Id).FirstOrDefault();
            if (existingMachine != null)
            {
                machine.Id = Id;
                _collection.ReplaceOne(m => m.Id == Id, machine);
            }
        }


        public void DeleteMachine(string machineId)
        {
            _collection.DeleteOne(m => m.Id == machineId);
            _machineAssetCollection.DeleteMany(ma => ma.Id == machineId);
        }
        
        public override void InsertMany(List<Machine> data)
        {
            var existingMachines = _collection.Find(machine => true).ToList();
            if (existingMachines.Count == 0)
            {
                _collection.InsertMany(data);
            }
        }

        public Machine GetMachineById(string machineId)
        {
            return _collection.Find(m => m.Id == machineId).FirstOrDefault();
        }
    }
}
