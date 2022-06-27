using DataLayer.Repositories;


namespace DataLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IArtworkRepository Artworks { get; }
        IRestaurationRepository Restaurations { get; }
        ILendingRepository Lendings { get; }
        IMuseumRepository Museums {get;}
        IDeletedUsersRepository DeletedUsers{get;}
        int Complete();
    }
}

