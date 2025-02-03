using MachineAssetTracker.Data;
using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;
using System.Reflection.PortableExecutable;
using Machine = MachineAssetTracker.Models.Machine;


namespace MachineAssetTracker.Services
{
    public class MachineAssetsService : IMachineAssetsService
    {
        private static MachineAssetData _machineAssets = new MachineAssetData();
        private static MachineData _machines = new MachineData();
        private static AssetData _assets = new AssetData();
        List<MachineAsset> machineAssets= _machineAssets.GetAll();
        List<Machine> machines = _machines.GetAll();
        List<Asset> assets = _assets.GetAll();

        public List<MachineAsset> GetAll()
        {
            return machineAssets;
        }

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
            var latestSeries = assets.ToDictionary(a => a.AssetName, a => a.Series.Max(s => int.Parse(s.Substring(1))));
            return machines
                    .Where(machine =>
                        machine.Assets.All(asset =>
                            latestSeries.ContainsKey(asset.AssetName) &&
                            asset.Series.All(series =>
                            int.Parse(series.Substring(1)) == latestSeries[asset.AssetName])))
                           .Select(machine => machine.MachineType).ToList();
        }
    }
}
