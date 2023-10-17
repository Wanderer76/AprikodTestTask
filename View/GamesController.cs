using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace View
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetGameList(string? genre, int offset = 0, int limit = 20)
        {
            try
            {
                List<GameDto> result = string.IsNullOrWhiteSpace(genre)
                    ? await _gameService.GetGames(offset, limit)
                    : await _gameService.GetGamesByGenre(genre, offset, limit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGame([FromBody] GameDto gameDto)
        {
            try
            {
                var result = await _gameService.CreateGame(gameDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditGame([FromBody] GameDto gameDto)
        {
            try
            {
                var result = await _gameService.EditGame(gameDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{gameId:guid}")]
        public async Task<IActionResult> DeleteGame(Guid gameId)
        {
            try
            {
                await _gameService.DeleteGame(gameId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}