
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

public class RestaurationRepository : Repository<Restauration>, IRestaurationRepository
{
    public RestaurationRepository(DbContext context) : base(context)
    {
    }

    public Restauration GetById(int id, int artworkid) => Context.Set<Restauration>().Find(id,artworkid);
    
}


