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

        public MuseumManagementContext MuseumManagementContext {
            get { return Context as MuseumManagementContext;}
        }
    }
}