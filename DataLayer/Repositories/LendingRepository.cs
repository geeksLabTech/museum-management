using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

public class LendingToMuseumRepository : Repository<LendingToMuseum>, ILendingRepository
{
    public LendingToMuseumRepository(DbContext context) : base(context)
    {
    }
}

