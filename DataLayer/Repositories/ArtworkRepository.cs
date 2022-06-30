using DataLayer.Models;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ArtworkRepository : Repository<Artwork>, IArtworkRepository
    {
        public ArtworkRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Artwork> GetArtworksByRoom(string room)
        {
            return this.Find(x => x.MuseumRoom == room);
        }

        public IEnumerable<Artwork> GetArtworksByMuseumId(int museumId)
        {
            return this.Find(x => x.ActualMuseumId == museumId);
        }

        public IEnumerable<Artwork> GetAllArtworksOfOtherMuseums(){
            return this.Find(x => x.ActualMuseumId != 3);
        }



        public MuseumManagementContext MuseumManagementContext {
            get { return Context as MuseumManagementContext;}
        }
    }
}