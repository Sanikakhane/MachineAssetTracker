using MachineAssetTracker.Models;

namespace MachineAssetTracker.Interfaces
{
    public interface IMachineAssetsService
    {
        List<MachineAsset> GetAll();
        List<string> GetAssetsByMachineType(string machineType);
        List<string> GetMachinesByAsset(string assetName);
        List<string> GetMachinesUsingLatestSeries();
    }
}
