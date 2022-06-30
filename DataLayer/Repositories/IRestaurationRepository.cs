
using DataLayer.Models;

public interface IRestaurationRepository : IRepository<Restauration> {

     public Restauration GetById(int id, int artworkid);

}
