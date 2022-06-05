using crudproject.DataAccessLayer.infrastructure.iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudproject.DataAccessLayer.infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        // public ICategoryRepository Category => throw new NotImplementedException();

        //public IProductRepository Product => throw new NotImplementedException();

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            Category = new CategoryRepository(db);
            Product = new ProductRepository(db);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}