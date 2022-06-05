using crudproject.DataAccessLayer.infrastructure.iRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace crudproject.DataAccessLayer.infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        private readonly DbSet<T> dbset;

        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            dbset = db.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public T GetT(Expression<Func<T, bool>> predicate)
        {
            return dbset.Where(predicate).FirstOrDefault();
        }
    }
}