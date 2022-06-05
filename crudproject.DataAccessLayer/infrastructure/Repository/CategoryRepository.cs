using crudproject.DataAccessLayer.infrastructure.iRepository;
using crudproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudproject.DataAccessLayer.infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(Category category)
        {
            //db.Categories.Update(category);
            var CategoryDB = db.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (CategoryDB != null)
            {
                CategoryDB.Name = category.Name;
                CategoryDB.Displayorder = category.Displayorder;
            }
        }
    }
}