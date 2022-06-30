

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;

    public Repository(DbContext context){
        Context = context;
    }

    public TEntity GetById(int id) => Context.Set<TEntity>().Find(id);
    // public TEntity GetLendingById(int artworkid, int museumid) => Context.Set<TEntity>().Find(artworkid,museumid);
    
    public IEnumerable<TEntity> GetAll() => Context.Set<TEntity>().ToList();

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate);
    }


    public void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
    }

    
    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}
