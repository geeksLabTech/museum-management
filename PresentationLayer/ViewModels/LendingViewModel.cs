


using DataLayer.Models;

namespace PresentationLayer.ViewModels{

    public class LendingViewModel {
        public int Id {get; set;}
        public string ArtworkTitle{get; set;}
        
        public string MuseumName {get; set;}

        public LendingToMuseum LendingToMuseum {get; set;}


    }


}

