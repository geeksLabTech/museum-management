using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

public class LendingToMuseumRepository : Repository<LendingToMuseum>, ILendingRepository
{
    public LendingToMuseumRepository(DbContext context) : base(context)
    {
    }
     public LendingToMuseum GetById(int artworkid, int museumid) => Context.Set<LendingToMuseum>().Find(artworkid,museumid);
     public IEnumerable<LendingToMuseum> GetLendingsByState(string state)
        {
            return this.Find(x => x.LendingState.ToString() == state);
        }

}

