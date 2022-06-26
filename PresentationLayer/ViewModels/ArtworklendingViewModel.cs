using DataLayer.Models;

namespace PresentationLayer.ViewModels{

    public class ArtworklendingViewModel {
        public int Id {get;set;}
        public string ArtworkTitle{get; set;}
        
        public string MuseumName {get; set;}
        public string Athor {get;set;}

        public LendingToMuseum LendingToMuseum {get; set;}


    }


}