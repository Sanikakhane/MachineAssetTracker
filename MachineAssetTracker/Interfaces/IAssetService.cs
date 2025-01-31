using MachineAssetTracker.Models;

namespace MachineAssetTracker.Interfaces
{
    public interface IAssetService
    {
        public List<Asset> GetAll();
        public void InsertAsset(Asset asset);
        public void UpdateAssetDetails(string Id, Asset asset);
        public void DeleteAsset(string id);
        public Asset GetAssetById(string id);
    }
}
