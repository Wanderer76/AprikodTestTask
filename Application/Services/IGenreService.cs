using Application.Dto;

namespace Application.Services
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetGenries(int offset,int limit);
        Task<GenreDto> CreateGenre(string name);
        Task<GenreDto> EditGenre(GenreDto genreDto);
        Task DeleteGenre(Guid genreId);
    }
}
