using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using DataLayer.Models;

namespace PresentationLayer.ViewModels{
    public class ArtworkViewModel{
        public Artwork? Artwork {get; set;}
      
        public List<Restauration>? Restaurations {get; set;}
        public SelectList? MuseumRoom { get; set;}

        public string ActualMuseum {get; set;}
        public string? ArtworkRoom { get; set; }
    }
}
