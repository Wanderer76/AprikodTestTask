using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class GenreDto
    {
        public required Guid Id { get; set; }
        [MinLength(3,ErrorMessage = "Жанр должен быть не короче 3х символов")]
        public required string Name { get; set; }
    }
}
