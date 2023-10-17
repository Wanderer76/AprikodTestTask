﻿using Shared.Domain;

namespace AprikodTestTask.Entities
{
    public class Genre : BaseEntity
    {
        public required string Name { get; set; }
        public List<Game> Games { get; set; } = new();
    }
}
