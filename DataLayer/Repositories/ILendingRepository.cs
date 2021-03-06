using DataLayer.Models;

public interface ILendingRepository : IRepository<LendingToMuseum> {
     public LendingToMuseum GetById(int artworkid, int museumid);
     public IEnumerable<LendingToMuseum> GetLendingsByState (string state);

}