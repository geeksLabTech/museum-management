using DataLayer.Models;

namespace PresentationLayer.ViewModels{

    public class ArtworklendingViewModel {
    
        public int artworkid {get; set;}
        public int museumid {get; set;}
        public string ArtworkTitle{get; set;}
        
        public string MuseumName {get; set;}
        public string Author {get;set;}

        public LendingToMuseum LendingToMuseum {get; set;}


    }


}