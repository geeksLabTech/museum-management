using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels {

    public class AddArtworkViewModel
    {
        
        
        public string Title {get; set;}


        
        public string Author {get;set;}


        
        public int EconomicValue {get; set;}


        
        public string Period {get; set;}

        [DataType(DataType.DateTime)]
        
        public DateTime CreationDate {get;set;}

        // [DataType(DataType.DateTime)]
        // [Required(ErrorMessage = "Entry Date is required")]
        // public DateTime EntryDate {get;set;}


        
        [Required(ErrorMessage = "Museum room is required")]
        public string MuseumRoom {get; set;}
    }
}