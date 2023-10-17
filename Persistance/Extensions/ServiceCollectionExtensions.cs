using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistance.Repositories;
using Shared.Persistance;

namespace Persistance.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DefaultConnection:Postgres"];
            services.AddDbContext<GameDbContext>(options =>
            {
                options
                .UseNpgsql(connectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
            });
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IDbInitializer, GameDbInitializer>();
        }
    }
}
