using MachineAssetTracker.Models;
using MongoDB.Driver;

namespace MachineAssetTracker.Data
{
    public class AssetData : MongoDBContextBase<Asset>
    {
        public AssetData() : base("Assets") { }

        public override List<Asset> GetAll()
        {
            return _collection.Find(asset => true).ToList();
        }

        public override void InsertMany(List<Asset> data)
        {
            var existingAssets = _collection.Find(asset => true).ToList();
            if (existingAssets.Count == 0)
            {
                _collection.InsertMany(data);
            }
        }
        public void InsertAsset(Asset asset)
        {
            var existingAsset = _collection.Find(a => a.AssetName == asset.AssetName).FirstOrDefault();
            if (existingAsset == null)
            {
                _collection.InsertOne(asset);
            }
        }
        public void UpdateAsset( Asset asset)
        {
            var existingAsset = _collection.Find(a => a.AssetName == asset.AssetName).FirstOrDefault();
            if (existingAsset == null)
            {
                _collection.InsertOne(asset);
            }
            else
            {
                foreach (var series in asset.Series)
                {
                    if (!existingAsset.Series.Contains(series))
                    {
                        existingAsset.Series.Add(series);
                    }
                    _collection.ReplaceOne(a => a.AssetName == asset.AssetName, existingAsset);
                }
            }
        }
        public Asset GetAssetById(string id)
        {
            return _collection.Find(a => a.Id == id).FirstOrDefault();
        }
        public void DeleteAsset(string id)
        {
            _collection.DeleteOne(a => a.Id == id);
        }
    }
}
