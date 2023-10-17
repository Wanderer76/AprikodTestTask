using AprikodTestTask.Entities;

namespace Persistance.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> GetGenreById(Guid genreId);
        Task<Genre> GetGenreByName(string name);
        Task<Genre> GetGenreByNameOrCreate(string name);
        Task<List<Genre>> GetGenries(int offset, int limit);
        Task<Genre> Update(Genre genre);
        Task<Genre> Create(string name);
        Task DeleteGenre(Guid genreId);
    }
}
