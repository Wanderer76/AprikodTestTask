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

        public async Task Delete(Guid id)
        {
            var game = await _dbContext.Games.FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
                throw new ArgumentException($"Игры с id = {id} не существует");
            _dbContext.Games.Remove(game);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Game> GetGameById(Guid id)
        {
            var game = await _dbContext.Games.Include(game => game.Genres).FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
                throw new ArgumentException($"Игры с id = {id} не существует");
            return game;
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
            //var game = await _dbContext.Games.FirstOrDefaultAsync(x => x.Id == gameDto.Id);
            //if (game == null)
            //    throw new ArgumentException($"Игры с id = {gameDto.Id} не существует");

            _dbContext.Games.Update(gameDto);
            await _dbContext.SaveChangesAsync();
            return gameDto;
        }
    }
}
