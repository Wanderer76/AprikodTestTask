using Application.Dto;

namespace Application.Services
{
    public interface IGameService
    {
        Task<GameDto> CreateGame(GameDto gameDto);
        Task<GameDto> EditGame(GameDto gameDto);
        Task DeleteGame(Guid gameId);
        Task<List<GameDto>> GetGames(int offset, int limit);
        Task<List<GameDto>> GetGamesByGenre(string genreName, int offset, int limit);
    }
}
