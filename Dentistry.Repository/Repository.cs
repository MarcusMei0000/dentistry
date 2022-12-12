using System.Linq.Expressions;
using Dentistry.Entities.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

namespace Dentistry.Repository;

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    private Context _context;
    private ILogger<Repository<T>> logger;

    public Repository(Context context, ILogger<Repository<T>> logger)
    {
        _context = context;
        this.logger = logger;
    }
    public void Delete(T obj)
    {
        _context.Set<T>().Attach(obj);
        _context.Entry(obj).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate);
    }

    public T GetById(Guid id)
    {
        return _context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    private T Insert(T obj)
    {
        obj.Init();
        var result = _context.Set<T>().Add(obj);
        _context.SaveChanges();
        return result.Entity;
    }

    private T Update(T obj)
    {
        obj.ModificationTime = DateTime.UtcNow;
        var result = _context.Set<T>().Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
        _context.SaveChanges();
        return result.Entity;
    }

    public T Save(T obj)
    {
        try
        {
            if (obj.IsNew())
            {
                return Insert(obj);
            }
            else
            {
                return Update(obj);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            throw ex;
        }
    }
}

