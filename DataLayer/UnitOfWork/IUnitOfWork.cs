using DataLayer.Repositories;


namespace DataLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IArtworkRepository Artworks { get; }
        IRestaurationRepository Restaurations { get; }
        ILendingRepository Lendings { get; }
        int Complete();
    }
}

