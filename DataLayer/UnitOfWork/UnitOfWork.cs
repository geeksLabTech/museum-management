
using DataLayer.Repositories;
using DataLayer.Data;
using DataLayer.UnitOfWork;

namespace Datalayer.UnitOfWork {
    public class UnitOfWork : IUnitOfWork {
        private readonly MuseumManagementContext _context;
        
        public UnitOfWork(MuseumManagementContext context) {
            _context = context;
            Artworks = new ArtworkRepository(_context);
            Restaurations = new RestaurationRepository(_context);
            Lendings = new LendingToMuseumRepository(_context);
            Museums = new MuseumsRepository(_context);
        }
        
        public IArtworkRepository Artworks {get; private set;}

        public IRestaurationRepository Restaurations {get; private set;}

        public ILendingRepository Lendings {get; private set;}

        public IMuseumRepository Museums {get; private set;}

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }

}
