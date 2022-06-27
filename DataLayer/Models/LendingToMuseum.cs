using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models{
    public class LendingToMuseum{
        // public int Id { get; set; }

        public int ArtworkId {get; set;}

        public int MuseumId {get; set;}

        public Artwork Artwork {get; set;}

        public Museum Museum {get; set;}

        public int Amount {get; set;}
        /*
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate {get; set;}
        */

        public String LendingState {get; set;}

        public int PeriodInDays {get; set;}
    }
}

public static class LendingState {
    public const string Requested = "Requested";
    public const string Lended = "Lended";
    public const string Denied = "Denied";
    public const string Returned = "Returned";
    
}