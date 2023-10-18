using Microsoft.EntityFrameworkCore;
using Shared.Persistance;

namespace Persistance
{
    public class GameDbInitializer : IDbInitializer
    {
        private readonly GameDbContext _gameDbContext;

        public GameDbInitializer(GameDbContext gameDbContext)
        {
            _gameDbContext = gameDbContext;
        }

        public async Task Migrate()
        {
            await _gameDbContext.Database.MigrateAsync();
        }
    }
}
