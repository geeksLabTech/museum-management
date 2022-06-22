
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

public class RestaurationRepository : Repository<Restauration>, IRestaurationRepository
{
    public RestaurationRepository(DbContext context) : base(context)
    {
    }
}


