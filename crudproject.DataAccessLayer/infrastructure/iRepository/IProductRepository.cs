using crudproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudproject.DataAccessLayer.infrastructure.iRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}