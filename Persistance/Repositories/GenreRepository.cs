using AprikodTestTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories
{
    internal class GenreRepository : IGenreRepository
    {
        private readonly GameDbContext _gameDbContext;

        public GenreRepository(GameDbContext gameDbContext)
        {
            _gameDbContext = gameDbContext;
        }

        public async Task<Genre> Create(string name)
        {
            var genre = new Genre { Name = name };
            await _gameDbContext.Genres.AddAsync(genre);
            await _gameDbContext.SaveChangesAsync();
            return genre;
        }

        public async Task DeleteGenre(Genre genre)
        {
            _gameDbContext.Genres.Remove(genre);
            await _gameDbContext.SaveChangesAsync();
        }

        public async Task<Genre?> GetGenreById(Guid genreId)
        {
            return await _gameDbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == genreId);
        }

        public async Task<Genre?> GetGenreByName(string name)
        {
            return await _gameDbContext.Genres.FirstOrDefaultAsync(genre => genre.Name == name);
        }

        public async Task<Genre> GetGenreByNameOrCreate(string name)
        {
            var genre = await _gameDbContext.Genres.FirstOrDefaultAsync(genre => genre.Name == name);
            if (genre == null)
            {
                genre = new Genre { Name = name };
                await _gameDbContext.Genres.AddAsync(genre);
                await _gameDbContext.SaveChangesAsync();
            }
            return genre;
        }

        public async Task<List<Genre>> GetGenries(int offset, int limit)
        {
            return await _gameDbContext.Genres.Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<Genre> Update(Genre genre)
        {
            _gameDbContext.Genres.Update(genre);
            await _gameDbContext.SaveChangesAsync();
            return genre;
        }
    }
}
