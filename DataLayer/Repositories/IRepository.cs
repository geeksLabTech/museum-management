
using System.Linq.Expressions;

public interface IRepository<TEntity> where TEntity : class {
    public TEntity GetById(int id);
    public IEnumerable<TEntity> GetAll();
    public IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> predicate);

    public void Add(TEntity entity);
    public void AddRange(IEnumerable<TEntity> entities);

    public void Remove(TEntity entity);
    public void RemoveRange(IEnumerable<TEntity> entities);
}