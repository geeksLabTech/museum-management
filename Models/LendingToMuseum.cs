using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace museum_management.Models{
    public class LendingToMuseum{
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

        public LendingState LendingState {get; set;}

        public int PeriodInDays {get; set;}
    }
}

public enum LendingState {
    Requested,
    Lended,
    Denied,
    Returned,
    
}