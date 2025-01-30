namespace MachineAssetTracker.Interfaces
{
    public interface IMachineAssetsService
    {
        List<string> GetAssetsByMachineType(string machineType);
        object? GetMachineAssets();
        List<string> GetMachinesByAsset(string assetName);
        List<string> GetMachinesUsingLatestSeries();
    }
}
