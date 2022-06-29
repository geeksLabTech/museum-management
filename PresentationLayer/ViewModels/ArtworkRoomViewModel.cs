using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using DataLayer.Models;

namespace PresentationLayer.ViewModels
{
    public class ArtworkRoomViewModel
    {
        public List<Artwork>? Artworks {get; set;}
        public SelectList? MuseumRoom { get; set; }
        public string? Artworkroom { get; set; }
        public List<DateTime> LastResaturation{get;set;} 
    }
}