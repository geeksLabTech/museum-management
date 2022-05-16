

namespace museum_management.Models{
    public class ArtworkViewModel{
        public Artwork Artwork {get; set;}
        
        public List<Restauration> Restaurations {get; set;}

        public string ActualMuseum {get; set;}
    }
}
