using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DataLayer.Models{
    public class Artwork {
        public int Id {get; set;}

        [StringLength(60, MinimumLength = 3)]
        [Required(AllowEmptyStrings =true)]
        
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

        public List<Restauration> Restaurations {get; set;}

        // [Display(nameof = "Actual Museum")]
        // public string? ActualMuseum {get; set;}
    }
}

public class ValidDate : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext) =>
    DateTime.TryParseExact(value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
        ? ValidationResult.Success
        : new ValidationResult("Invalid date, please try again with a valid date in the format of DD/MM/YYYY.");
}