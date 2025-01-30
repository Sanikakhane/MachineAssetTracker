using MachineAssetTracker.Components;
using MachineAssetTracker.Data;
using MongoDB.Driver;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

/// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Log to console (optional)
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)  // Log files that roll daily
    .CreateLogger();

// Add Serilog to the logging pipeline
builder.Logging.ClearProviders();  // Clear other providers (if any)
builder.Logging.AddSerilog();  // Add Serilog for logging
builder.Services.AddSingleton<MongoDBContext>();
builder.Services.AddHostedService<DataLoader>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
