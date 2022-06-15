using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models{
    public class Picture : Artwork {
        public string? Style {get; set;}

        public string? Technique {get; set;}
    }
}