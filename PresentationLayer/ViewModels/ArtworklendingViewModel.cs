using DataLayer.Models;

namespace PresentationLayer.ViewModels{

    public class ArtworklendingViewModel {
    
        public int ArtworkId {get; set;}
        public int MuseumId {get; set;}
        public string ArtworkTitle{get; set;}
        
        public string MuseumName {get; set;}
        public string Author {get;set;}

        public LendingToMuseum LendingToMuseum {get; set;}


    }


}