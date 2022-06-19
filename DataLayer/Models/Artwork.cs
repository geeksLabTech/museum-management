using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models{
    public class Artwork {
        public int Id {get; set;}

        public string Title {get; set;}

        public string? Author {get; set;}

        [Display(Name = "Ecnomic Value")]
        public int EconomicValue {get; set;}

        public string Period {get; set;}


        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime CreationDate {get; set;}

        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate {get; set;}

        [Display(Name = "Museum room")]
        public string? MuseumRoom {get;set;}

        public List<LendingToMuseum> LendingToMuseums {get; set;}

        // [Display(nameof = "Actual Museum")]
        // public string? ActualMuseum {get; set;}
    }
}