using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAOs;

public class GenericDAO<TEntity, TKey> where TEntity : class
{
    private readonly SdcrpDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericDAO(SdcrpDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public bool Delete(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public TEntity GetById(TKey id)
    {
        return _dbSet.Find(id);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }
    public ICollection<TEntity> GetListByProperty(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).ToList();
    }
    public IQueryable<TEntity> GetByIdWithInclude(
        Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return query;
    }
}