using AprikodTestTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories
{
    internal class GameRepository : IGameRepository
    {
        private readonly GameDbContext _dbContext;

        public GameRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Game> Create(Game gameDto)
        {
            await _dbContext.AddAsync(gameDto);
            await _dbContext.SaveChangesAsync();
            return gameDto;
        }

        public async Task Delete(Game game)
        {
            _dbContext.Games.Remove(game);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Game?> GetGameById(Guid id)
        {
            return await _dbContext.Games
                .Include(game => game.Genres)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Game?> GetGameByName(string name)
        {
            return await _dbContext.Games
                .Include(game => game.Genres)
                .FirstOrDefaultAsync(game => game.Name == name);
        }

        public async Task<List<Game>> GetGames(int offset, int limit)
        {
            return await _dbContext.Games
                .Skip(offset)
                .Take(limit)
                .Include(game => game.Genres)
                .ToListAsync();
        }

        public async Task<List<Game>> GetGamesByGenre(Genre genre, int offset, int limit)
        {
            return await _dbContext.Games
                .Include(game => game.Genres)
                .Where(game => game.Genres.Contains(genre))
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Game> Update(Game gameDto)
        {
            _dbContext.Games.Update(gameDto);
            await _dbContext.SaveChangesAsync();
            return gameDto;
        }
    }
}
