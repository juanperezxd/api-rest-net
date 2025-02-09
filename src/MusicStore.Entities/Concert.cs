﻿

namespace MusicStore.Entities
{
    public class Concert : EntityBase
    {

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Place {  get; set; } = default!;
        public double UnitPrice { get; set; }
        public int GenreId { get; set; }
        public DateTime DateEvent { get; set; }
        public string? ImageUrl { get; set; } 
        public int TicketsQuantify { get; set; }
        public bool Finalized { get; set; }
        public virtual Genre Genre { get; set; } = default!;

    }
}
