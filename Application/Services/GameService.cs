using Application.Dto;
using AprikodTestTask.Entities;
using AutoMapper;
using Persistance.Repositories;

namespace Application.Services
{
    internal class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper, IGenreRepository genreRepository)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        public async Task<GameDto> CreateGame(GameDto gameDto)
        {
            if ((await _gameRepository.GetGameByName(gameDto.Name)) != null)
                throw new ArgumentException($"Игра с названием {gameDto.Name} уже существует");

            var game = _mapper.Map<Game>(gameDto);
            game.Id = Guid.NewGuid();
            var genres = new List<Genre>(gameDto.Genres.Count);
            foreach (var genre in gameDto.Genres)
                genres.Add(await _genreRepository.GetGenreByNameOrCreate(genre));
            game.Genres = genres;
            var result = await _gameRepository.Create(game);
            return _mapper.Map<GameDto>(result);
        }

        public async Task DeleteGame(Guid gameId)
        {
            var game = await _gameRepository.GetGameById(gameId) ?? throw new ArgumentException($"Игры с id = {gameId} не существует");
            await _gameRepository.Delete(game);
        }

        public async Task<List<GameDto>> GetGames(int offset, int limit)
        {
            var result = await _gameRepository.GetGames(offset, limit);
            return result
                .Select(game => _mapper.Map<GameDto>(game))
                .ToList();
        }

        public async Task<List<GameDto>> GetGamesByGenre(string genreName, int offset, int limit)
        {
            var genre = await _genreRepository.GetGenreByName(genreName);
            if (genre == null)
                throw new ArgumentException($"Жанра с названием = {genreName} не существует");
            var result = await _gameRepository.GetGamesByGenre(genre, offset, limit);
            return result
                .Select(game => _mapper.Map<GameDto>(game))
                .ToList();
        }

        public async Task<GameDto> EditGame(GameDto gameDto)
        {
            var game = await _gameRepository.GetGameById(gameDto.Id) ?? throw new ArgumentException($"Игры с id = {gameDto.Id} не существует");

            if (game.Name != gameDto.Name && (await _gameRepository.GetGameByName(gameDto.Name)) != null)
                throw new ArgumentException($"Игра с названием {gameDto.Name} уже существует");

            var genres = new List<Genre>(gameDto.Genres.Count);
            foreach (var genre in gameDto.Genres)
                genres.Add(await _genreRepository.GetGenreByNameOrCreate(genre));
            game.Name = gameDto.Name;
            game.Developer = gameDto.Developer;
            game.Genres = genres;

            var result = await _gameRepository.Update(game);
            return _mapper.Map<GameDto>(result);
        }
    }
}
