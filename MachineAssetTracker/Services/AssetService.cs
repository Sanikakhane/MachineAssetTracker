using MachineAssetTracker.Data;
using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;

namespace MachineAssetTracker.Services
{
    public class AssetService : IAssetService
    {
        private AssetData _assetData = new AssetData();
        public void DeleteAsset(string id)
        {
            _assetData.DeleteAsset(id);
        }

        public List<Asset> GetAll()
        {
            return _assetData.GetAll();
        }

        public Asset GetAssetById(string id)
        {
            return _assetData.GetAssetById(id);
        }

        public string InsertAsset(Asset asset)
        {
            return _assetData.InsertAsset(asset);
        }

        public void UpdateAssetDetails(string Id, Asset asset)
        {
            _assetData.UpdateAsset(Id, asset);
        }
    }
}
