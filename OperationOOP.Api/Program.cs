using Microsoft.Extensions.Options;
using OperationOOP.Api.Endpoints;
using OperationOOP.Core.Data;
using OperationOOP.Core.Services;
using OperationOOP.Core.Models;

namespace OperationOOP.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName?.Replace('+', '.'));
                options.InferSecuritySchemes();
            });

            // Register tjänster som ex. BonsaiService och databasen
            builder.Services.AddSingleton<IDatabase, Database>();


            builder.Services.AddSingleton<BonsaiService>();

            var app = builder.Build();

            // HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Mappar alla endpoints
            app.MapEndpoints<Program>();

            // Seed data
            SeedData(app.Services);

            app.Run();
        }
        // Lägger till exempel på bonsaiträd i databasen
        private static void SeedData(IServiceProvider services)
        {
            var database = services.GetRequiredService<IDatabase>();

            
            database.Bonsais.Add(new Bonsai
            {
                Id = 1,
                Name = "Pine Bonsai",
                Species = "Pine",
                AgeYears = 5,
                LastWatered = DateTime.Now.AddDays(-10),
                LastPruned = DateTime.Now.AddDays(-5),
                Style = BonsaiStyle.Chokkan,
                CareLevel = CareLevel.Intermediate
            });

            database.Bonsais.Add(new Bonsai
            {
                Id = 2,
                Name = "Jade Bonsai",
                Species = "Jade",
                AgeYears = 3,
                LastWatered = DateTime.Now.AddDays(-3),
                LastPruned = DateTime.Now.AddDays(-1),
                Style = BonsaiStyle.Moyogi,
                CareLevel = CareLevel.Beginner
            });

            database.Bonsais.Add(new Bonsai
            {
                Id = 3,
                Name = "Maple Bonsai",
                Species = "Maple",
                AgeYears = 10,
                LastWatered = DateTime.Now.AddDays(-8),
                LastPruned = DateTime.Now.AddDays(-2),
                Style = BonsaiStyle.Kengai,
                CareLevel = CareLevel.Advanced
            });

            database.Bonsais.Add(new Bonsai
            {
                Id = 4,
                Name = "Semi Cascade Bonsai",
                Species = "HanKengai",
                AgeYears = 12,
                LastWatered = DateTime.Now.AddDays(-6),
                LastPruned = DateTime.Now.AddDays(-4),
                Style = BonsaiStyle.HanKengai,
                CareLevel = CareLevel.Master
            });

        }
    }
}