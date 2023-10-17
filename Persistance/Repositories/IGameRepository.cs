﻿using AprikodTestTask.Entities;

namespace Persistance.Repositories
{
    public interface IGameRepository
    {
        Task<Game> Create(Game gameDto);
        Task Delete(Guid id);
        Task<Game> Update(Game gameDto);
        Task<Game> GetGameById(Guid id);
        Task<List<Game>> GetGames(int offset, int limit);
        Task<List<Game>> GetGamesByGenre(Genre genre, int offset, int limit);
    }
}
