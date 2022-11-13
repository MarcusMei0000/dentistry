using System.Linq.Expressions;
using Dentistry.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dentistry.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private DbContext _context;
    private ILogger<Repository<T>> logger;

    public Repository(DbContext context, ILogger<Repository<T>> logger)
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

    public T Save(T obj)
    {
        try
        {
            if (obj.IsNew())
            {
                obj.Init();
                var result = _context.Set<T>().Add(obj);
                _context.SaveChanges();
                return result.Entity;
            }
            else
            {
                obj.ModificationTime = DateTime.UtcNow;
                var result = _context.Set<T>().Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();
                return result.Entity;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            throw ex;
        }
    }
}

