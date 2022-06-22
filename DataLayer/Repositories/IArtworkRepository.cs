using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IArtworkRepository : IRepository<Artwork>
    {
        public IEnumerable<Artwork> GetArtworksByRoom (String room);
    }
}