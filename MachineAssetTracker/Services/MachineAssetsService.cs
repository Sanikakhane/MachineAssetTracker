using MachineAssetTracker.Data;
using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;


namespace MachineAssetTracker.Services
{
    public class MachineAssetsService : IMachineAssetsService
    {
        private static MongoDBContext _mongoDbContext = new MongoDBContext();
        List<MachineAsset> machineAssets= _mongoDbContext.GetAll();
        public List<string> GetAssetsByMachineType(string machineType)
        {
            return machineAssets.Where(a => a.MachineType == machineType).Select(a => a.Asset).Distinct().ToList();
        }

        public object? GetMachineAssets()
        {
            return machineAssets;
        }

        public List<string> GetMachinesByAsset(string assetName)
        {
            return machineAssets.Where(a => a.Asset == assetName).Select(a => a.MachineType).Distinct().ToList();
        }

        public List<string> GetMachinesUsingLatestSeries()
        {
            var latestSeries = machineAssets.GroupBy(ma => ma.Asset)
                                             .ToDictionary( g => g.Key,
                                              g => g.Max(ma => int.Parse(ma.Series.Substring(1)))); 


            // Get machines that are using the latest series for all assets
            return machineAssets
                .GroupBy(ma => ma.MachineType)
                .Where(g =>
                    g.All(ma =>
                        int.Parse(ma.Series.Substring(1)) == latestSeries[ma.Asset] // Check if all series match latest for the asset
                    )
                )
                .Select(g => g.Key)
                .ToList();
        }
    }
}
