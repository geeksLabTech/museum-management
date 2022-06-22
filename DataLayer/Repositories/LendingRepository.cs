using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

public class LendingToMuseumRepository : Repository<LendingToMuseum>
{
    public LendingToMuseumRepository(DbContext context) : base(context)
    {
    }
}

