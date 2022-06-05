using crudproject.DataAccessLayer.infrastructure.iRepository;
using crudproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudproject.DataAccessLayer.infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(Product product)
        {
            //db.Products.Update(product);

            var ProductDB = db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (ProductDB != null)
            {
                ProductDB.Name = product.Name;
                ProductDB.Description = product.Description;
                ProductDB.Price = product.Price;

                if (ProductDB.ImageUrl != null)
                {
                    ProductDB.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}