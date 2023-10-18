using Application.Dto;
using AprikodTestTask.Entities;
using AutoMapper;
using Persistance;
using Persistance.Repositories;
using System.Xml.Linq;

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
            if ((await _genreRepository.GetGenreByName(name)) != null)
                throw new ArgumentException($"Жанр с названием = {name} уже существует");
            var genre = await _genreRepository.Create(name);
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> EditGenre(GenreDto genreDto)
        {
            var genre = await _genreRepository.GetGenreById(genreDto.Id) ?? throw new ArgumentException($"Жанра с id = {genreDto.Id} не существует");
            if (genre.Name != genreDto.Name && (await _genreRepository.GetGenreByName(genreDto.Name)) != null)
                throw new ArgumentException($"Жанр с названием = {genreDto.Name} уже существует");

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
            var genre = await _genreRepository.GetGenreById(id) ?? throw new ArgumentException($"Жанра с id = {id} не существует");
            await _genreRepository.DeleteGenre(genre);
        }
    }
}
