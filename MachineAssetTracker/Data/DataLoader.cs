using MachineAssetTracker.Data;
using MachineAssetTracker.Models;


public class DataLoader : IHostedService
{
    private readonly MongoDBContext _machineAssests = new MongoDBContext();
    private readonly MongoDBContext _machines = new MongoDBContext();
    private readonly MongoDBContext _assets = new MongoDBContext();

    private readonly ILogger<DataLoader> _logger;
    private const string FilePath = "C:\\Users\\Khan_San\\source\\repos\\MachineAssetTracker\\MachineAssetTracker\\Data\\matrix.txt";  

    public DataLoader(MongoDBContext mongoDbContext, ILogger<DataLoader> logger) 
    {
        _machineAssests = mongoDbContext;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting data loader service...");

        if (File.Exists(FilePath))
        {
            //Adding the data to in MachineAsset collection
            var machineAssets = File.ReadAllLines(FilePath)
                .Select(line => line.Split(','))
                .Where(parts => parts.Length == 3)
                .Select(parts => new MachineAsset
                {
                    MachineType = parts[0].Trim(),
                    
                    Asset = parts[1].Trim(),
                    Series = parts[2].Trim()
                }).ToList();
            if(machineAssets.Count == 0)
            {
                throw new ArgumentException($"No data found in file {FilePath}!");
            }
             _machineAssests.InsertMachineAssets(machineAssets);  // Using the injected MongoDBContext
            _logger.LogInformation("Machine asset data successfully loaded into MongoDB.");

            //Adding the data to in Machine collection
            var machines = machineAssets
                        .GroupBy(ma => ma.MachineType)
                        .Select(g => new Machine
                        {
                            MachineType = g.Key,
                            Assets = g.Select(ma => new Asset
                            {
                                AssetName = ma.Asset,
                                Series = new List<string> { ma.Series }  
                            }).ToList()
                        }).ToList();
            _machines.InsertMachines(machines);

            //Adding the data to asset collection
            var assets = machineAssets
                .GroupBy(ma => ma.Asset)
                .Select(g => new Asset
                {
                    AssetName = g.Key,
                    Series = g.Select(ma => ma.Series).ToList()
                }).ToList();
            _assets.InsertAssets(assets);


        }
        else
        {
            throw new ArgumentException($"File {FilePath} not found!");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping data loader service...");
        return Task.CompletedTask;
    }
}