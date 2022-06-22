
using DataLayer.Repositories;
using DataLayer.Data;
using DataLayer.UnitOfWork;

namespace Datalayer.UnitOfWork {
    public class UnitOfWork : IUnitOfWork {
        private readonly MuseumManagementContext _context;
        private IArtworkRepository _artworks;
        private IRestaurationRepository _restaurations;
        private ILendingRepository _lendings;
        private bool disposedValue;

        public IArtworkRepository Artworks {get; private set;}

        public IRestaurationRepository Restaurations {get; private set;}

        public ILendingRepository Lendings {get; private set;}

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
