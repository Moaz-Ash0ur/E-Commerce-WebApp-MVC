using ECommerceStore.DAL.Data;
using ECommerceStore.Entities.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceStore.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public T? GetById(int id) => _dbSet.Find(id);

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        IQueryable<T> IGenericRepository<T>.GetAllInclude(params string[] agers)
        {
            IQueryable<T> query = _dbSet;

            if (agers.Length > 0)
            {
                foreach (var item in agers)
                {
                    query = query.Include(item);                  
                }
            }
            return query;
        }
    }



}

