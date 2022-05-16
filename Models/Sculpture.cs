using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace museum_management.Models{
    public class Sculpture : Artwork {
        public string Style {get; set;}

        public string Material {get; set;}
    }
}