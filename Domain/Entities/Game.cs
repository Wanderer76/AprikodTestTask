using Shared.Domain;
using System.ComponentModel.DataAnnotations;

namespace AprikodTestTask.Entities
{
    public class Game : BaseEntity
    {
        public required string Name { get; set; }
        public required string Developer { get; set; }
        public List<Genre> Genres { get; set; } = new();

    }
}
