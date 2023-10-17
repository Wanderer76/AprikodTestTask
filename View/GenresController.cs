using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace View
{
    [ApiController]
    [Route("[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGenre(string genreName)
        {
            var genre = await _genreService.CreateGenre(genreName);
            return Ok(genre);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditGenre([FromBody] GenreDto genreDto)
        {
            var genre = await _genreService.EditGenre(genreDto);
            return Ok(genre);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetGenries(int offset = 0, int limit = 20)
        {
            var result = await _genreService.GetGenries(offset, limit);
            return Ok(result);
        }
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            await _genreService.DeleteGenre(id);
            return Ok();
        }


    }
}
