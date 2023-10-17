using Application.Dto;
using AutoMapper;
using Persistance.Repositories;

namespace Application.Services
{
    internal class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<GenreDto> CreateGenre(string name)
        {
            var genre = await _genreRepository.Create(name);
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> EditGenre(GenreDto genreDto)
        {
            var genre = await _genreRepository.GetGenreById(genreDto.Id);
            genre.Name = genreDto.Name;
            await _genreRepository.Update(genre);
            return genreDto;
        }

        public async Task<List<GenreDto>> GetGenries(int offset, int limit)
        {
            var genries = await _genreRepository.GetGenries(offset, limit);
            return genries.Select(genre => _mapper.Map<GenreDto>(genre)).ToList();
        }
        public async Task DeleteGenre(Guid id)
        {
            await _genreRepository.DeleteGenre(id);
        }
    }
}
