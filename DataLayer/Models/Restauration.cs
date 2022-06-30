using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models{
    public class Restauration {
        public int Id {get; set;}

        [DataType(DataType.Date)]
        public DateTime StartDate {get; set;}

        [DataType(DataType.Date)]
        public DateTime EndDate {get; set;}
        public int ArtworkId {get; set;}
        public Artwork Artwork{get; set;}

        public string RestaurationType {get; set;}

    }

    public static class RestaurationType {
        public const string Complete = "Complete";
        public const string Partial = "Partial";
        public const string Minimal = "Minimal";
    }
}

