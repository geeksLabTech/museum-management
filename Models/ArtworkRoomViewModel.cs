using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace museum_management.Models
{
    public class ArtworkRoomViewModel
    {
        public List<Artwork> Artworks {get; set;}
        public SelectList? MuseumRoom { get; set; }
        public string? Artworkroom { get; set; }
    }
}