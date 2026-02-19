using Microsoft.EntityFrameworkCore;
using ProductionRouting.Application.Interfaces;
using ProductionRouting.Application.Services;
using ProductionRouting.Infrastructure.Configuration;
using ProductionRouting.Infrastructure.Persistence;
using ProductionRouting.Infrastructure.Services;

public partial class Program 
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ProductionRoutingDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IEvaluationService, EvaluationService>();
        builder.Services.AddScoped<IOrderProcessor, OrderProcessor>();
        builder.Services.AddHostedService<FileDropService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<ProductionRoutingDbContext>();

            var loader = new RulesetConfigLoader(context);

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Configuration",
                "RulesetConfig.json");

            loader.LoadFromFile(path);
        }

        app.Run();

    }


}