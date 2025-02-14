using MachineAssetTracker.Data;
using MachineAssetTracker.Models;


public class DataLoader : IHostedService
{
    private readonly MachineAssetData _machineAssests = new MachineAssetData();
    private readonly MachineData _machines = new MachineData();
    private readonly AssetData _assets = new AssetData();

    private readonly ILogger<DataLoader> _logger;
    private const string FilePath = "C:\\Users\\Khan_San\\source\\repos\\MachineAssetTracker\\MachineAssetTracker\\Data\\matrix.txt";  

    public static string? DataLoadError { get; private set; }
    public DataLoader(MachineAssetData mongoDbContext, ILogger<DataLoader> logger) 
    {
        _machineAssests = mongoDbContext;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting data loader service...");

        try
        {
            if(!File.Exists(FilePath))
            {
                DataLoadError = $"File Not Found: {FilePath}";
                _logger.LogError(DataLoadError);
                return;
            }
             var machineAssets = File.ReadAllLines(FilePath)
                    .Select(line => line.Split(','))
                    .Where(parts => parts.Length == 3)
                    .Select(parts => new MachineAsset
                    {
                        MachineType = parts[0].Trim().ToLower(),

                        Asset = parts[1].Trim().ToLower(),
                        Series = parts[2].Trim()
                    }).ToList();
                if (machineAssets.Count == 0)
                {
                    DataLoadError = $"No valid data found in file: {FilePath}";
                    _logger.LogError(DataLoadError);
                    return;
                }
                _machineAssests.InsertMany(machineAssets);
                _logger.LogInformation("Machine asset data successfully loaded into MongoDB.");

                //Adding the data to in Machine collection
                var machines = machineAssets
                            .GroupBy(ma => ma.MachineType)
                            .Select(g => new Machine
                            {
                                MachineType = g.Key.ToLower(),
                                Assets = g.Select(ma => new Asset
                                {
                                    AssetName = ma.Asset.ToLower(),
                                    Series = new List<string> { ma.Series }
                                }).ToList()
                            }).ToList();
                _machines.InsertMany(machines);

                //Adding the data to asset collection
                var assets = machineAssets
                    .GroupBy(ma => ma.Asset)
                    .Select(g => new Asset
                    {
                        AssetName = g.Key.ToLower(),
                        Series = g.Select(ma => ma.Series).ToList()
                    }).ToList();

                _assets.InsertMany(assets);    
        }
        catch (Exception ex)
        {
            DataLoadError = $"Error in DataLoader: {ex.Message}";
            _logger.LogError(DataLoadError);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping data loader service...");
        return Task.CompletedTask;
    }
}