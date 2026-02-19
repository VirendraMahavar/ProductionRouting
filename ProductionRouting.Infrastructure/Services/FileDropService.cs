using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductionRouting.Application.Interfaces;
using ProductionRouting.Domain.Models;
using System.IO.Compression;
using System.Text.Json;

namespace ProductionRouting.Infrastructure.Services;

public class FileDropService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<FileDropService> _logger;
    private readonly string _folderPath;

    public FileDropService(
        IServiceScopeFactory scopeFactory,
        ILogger<FileDropService> logger,
        IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _folderPath = configuration["FileDrop:FolderPath"] ?? "C:\\OrderDrop";
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FileDropService started. Monitoring {Path}", _folderPath);

        Directory.CreateDirectory(_folderPath);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var zipFiles = Directory.GetFiles(_folderPath, "*.zip");

                foreach (var zip in zipFiles)
                {
                    await ProcessZipAsync(zip);
                }

                var files = Directory.GetFiles(_folderPath, "*.json");

                foreach (var file in files)
                {
                    await ProcessFileAsync(file);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing file drop.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task ProcessFileAsync(string filePath)
    {
        try
        {
            _logger.LogInformation("Processing file {File}", filePath);

            var json = await File.ReadAllTextAsync(filePath);

            var order = JsonSerializer.Deserialize<Order>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (order == null)
                throw new Exception("Invalid JSON format");

            using var scope = _scopeFactory.CreateScope();
            var processor = scope.ServiceProvider
                .GetRequiredService<IOrderProcessor>();

            await processor.ProcessAsync(order);

            var processedPath = filePath + ".processed";
            File.Move(filePath, processedPath, overwrite: true);

            _logger.LogInformation("File processed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process file {File}", filePath);

            var errorPath = filePath + ".error";
            File.Move(filePath, errorPath, overwrite: true);
        }
    }

    private async Task ProcessZipAsync(string zipPath)
    {
        try
        {
            var extractPath = Path.Combine(
                _folderPath,
                Path.GetFileNameWithoutExtension(zipPath));

            ZipFile.ExtractToDirectory(zipPath, extractPath, true);

            var jsonFiles = Directory.GetFiles(extractPath, "*.json");

            foreach (var jsonFile in jsonFiles)
            {
                await ProcessFileAsync(jsonFile);
            }

            File.Move(zipPath, zipPath + ".processed", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ZIP processing failed");
            File.Move(zipPath, zipPath + ".error", true);
        }
    }
}
