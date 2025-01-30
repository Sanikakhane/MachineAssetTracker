using MachineAssetTracker.Models;
using MachineAssetTracker.Data;

public class DataLoader : IHostedService
{
    private readonly MongoDBContext _mongoDbContext = new MongoDBContext(); // Ensure MongoDBContext is injected
    private readonly ILogger<DataLoader> _logger;
    private const string FilePath = "C:\\Users\\Khan_San\\source\\repos\\MachineAssetTracker\\MachineAssetTracker\\Data\\matrix.txt";  // File location

    public DataLoader(MongoDBContext mongoDbContext, ILogger<DataLoader> logger)  // Inject MongoDBContext
    {
        _mongoDbContext = mongoDbContext;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting data loader service...");

        if (File.Exists(FilePath))
        {
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
            foreach (var machineAsset in machineAssets)
            {
                Console.WriteLine($"MachineType: {machineAsset.MachineType}, Asset: {machineAsset.Asset}, Series: {machineAsset.Series}");
            }
             _mongoDbContext.InsertMany(machineAssets);  // Using the injected MongoDBContext
            _logger.LogInformation("Machine asset data successfully loaded into MongoDB.");
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