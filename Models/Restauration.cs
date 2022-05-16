using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace museum_management.Models{
    public class Restauration {
        public int Id {get; set;}

        [DataType(DataType.Date)]
        public DateTime StartDate {get; set;}

        [DataType(DataType.Date)]
        public DateTime EndDate {get; set;}

        public int ArtworkId {get; set;}

        public Artwork Artwork{get; set;}
    }
}