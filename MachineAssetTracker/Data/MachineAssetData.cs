using MachineAssetTracker.Models;
using MongoDB.Driver;

namespace MachineAssetTracker.Data
{
    public class MachineAssetData : MongoDBContextBase<MachineAsset>
    {
        public MachineAssetData() : base("MachineAssets") { }

        public override List<MachineAsset> GetAll()
        {
            return _collection.Find(machineAsset => true).ToList();
        }

        public override void InsertMany(List<MachineAsset> data)
        {
            var existingMachineAssets = _collection.Find(machineAsset => true).ToList();
            if (existingMachineAssets.Count == 0)
            {
                _collection.InsertMany(data);
            }
        }
    }

}

