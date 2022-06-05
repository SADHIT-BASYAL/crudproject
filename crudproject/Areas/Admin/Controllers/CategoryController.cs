using crudproject.DataAccessLayer;
using crudproject.DataAccessLayer.infrastructure.iRepository;
using crudproject.Models;
using crudproject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace crudproject.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork UnitOfWork;

        public CategoryController(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categories = db.Categories;

            CategoryVM categoryVM = new CategoryVM();
            categoryVM.categories = UnitOfWork.Category.GetAll();
            return View(categoryVM);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UnitOfWork.Category.Add(category);
        //        UnitOfWork.Save();
        //        TempData["success"] = "Category Create Done";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        ////////////////////////////////////////         Edit            ////////////////////////////
        ///
        [HttpGet]
        public IActionResult createUpdate(int? id)
        {
            CategoryVM vm = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Category = UnitOfWork.Category.GetT(x => x.Id == id);
                if (vm.Category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult createUpdate(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Category.Id == 0)
                {
                    UnitOfWork.Category.Add(vm.Category);
                    TempData["success"] = "Category Created Done";
                }
                else
                {
                    UnitOfWork.Category.Update(vm.Category);
                }
                UnitOfWork.Save();
                TempData["success"] = "Category Updated Done";

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        ////////////////////////////////////////         Delete          ////////////////////////////
        ///
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = UnitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            var category = UnitOfWork.Category.GetT(x => x.Id == id);
            {
                if (category == null)
                {
                    return NotFound();
                }
                UnitOfWork.Category.Delete(category);
                UnitOfWork.Save();
                TempData["success"] = "Category Deleted Done";
            }
            return RedirectToAction("Index");
        }
    }
}