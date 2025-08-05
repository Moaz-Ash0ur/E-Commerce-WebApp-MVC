using ECommerceStore.DAL.Data;
using ECommerceStore.Entities.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        public IQueryable<T> GetAllAsQueryable();
        public IQueryable<T> GetAllInclude(params string[] agers);
      

    }



}

