using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(GameMapping));
            });
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGenreService, GenreService>();
        }
    }
}
