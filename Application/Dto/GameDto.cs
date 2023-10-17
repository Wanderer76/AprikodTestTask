using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class GameDto
    {
        public Guid Id { get; set; }
        [MinLength(3, ErrorMessage = "Название игры не должно быть меньше 3х символов")]
        public required string Name { get; set; }
        [MinLength(3, ErrorMessage = "Имя разработчика не должно быть меньше 3х символов")]
        public required string Developer { get; set; }
        [MinLength(1, ErrorMessage = "Игра должна содержать минимум 1 жанр")]
        public required List<string> Genres { get; set; }
    }
}
