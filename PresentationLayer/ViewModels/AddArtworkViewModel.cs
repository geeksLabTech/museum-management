using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.ViewModels {

    public class AddArtworkViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Title is required")]
        public string Title {get; set;}


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Author is required")]
        public string Author {get;set;}


        [DataType(DataType.Custom)]
        [Required(ErrorMessage = "Economic Values is required")]
        public int EconomicValue {get; set;}


        [Required(ErrorMessage = "Period is required")]
        public int Period {get; set;}

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage ="Creation Date is required")]
        public DateTime CreationDate {get;set;}

        // [DataType(DataType.DateTime)]
        // [Required(ErrorMessage = "Entry Date is required")]
        // public DateTime EntryDate {get;set;}


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Museum room is required")]
        public string MuseumRoom {get; set;}
    }
}