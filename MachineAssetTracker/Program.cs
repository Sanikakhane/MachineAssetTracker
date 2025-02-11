using MachineAssetTracker.Components;
using MachineAssetTracker.Interfaces;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MachineAssetTracker.Services;
using Serilog;
using MachineAssetTracker.Data;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddControllers();
// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Log to console (optional)
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)  // Log files that roll daily
    .CreateLogger();

// Add Serilog to the logging pipeline
builder.Logging.ClearProviders();  // Clear other providers (if any)
builder.Logging.AddSerilog();  // Add Serilog for logging
builder.Services.AddSingleton<MachineAssetData>();
builder.Services.AddHostedService<DataLoader>();
builder.Services.AddScoped<IMachineAssetsService, MachineAssetsService>();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<IAssetService, AssetService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        return new BadRequestObjectResult("Invalid Format");
    };
});


// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Machine Asset Tracker API", Version = "v1" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7053") // URL of your Blazor WASM app
                            .AllowAnyHeader()
                            .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Use Swagger UI in the middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}
app.MapControllers();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseCors("AllowSpecificOrigin");


app.MapRazorComponents<App>();

app.Run();
