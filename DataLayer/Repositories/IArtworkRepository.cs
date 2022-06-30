using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IArtworkRepository : IRepository<Artwork>
    {
        public IEnumerable<Artwork> GetArtworksByRoom (String room);

        public IEnumerable<Artwork> GetArtworksByMuseumId (int museumId);

        public IEnumerable<Artwork> GetAllArtworksOfOtherMuseums();
    }
}