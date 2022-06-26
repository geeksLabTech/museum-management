


using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.ViewModels{

    public class LendingViewModel {
        public string ArtworkTitle{get; set;}
        public int ArtworkId{get; set;}
        public int MuseumId{get;set;}
        public string MuseumName {get; set;}
        public SelectList? State {get; set;}
        public List<string> StateList {get; set;}
        public List<LendingToMuseum> Lendings{get;set;}
        public LendingToMuseum LendingToMuseum {get; set;}


    }


}

