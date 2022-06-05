using crudproject.DataAccessLayer;
using crudproject.DataAccessLayer.infrastructure.iRepository;
using crudproject.Models;
using crudproject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crudproject.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork UnitOfWork;
        private IWebHostEnvironment hostEnvironment;

        public ProductController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            this.UnitOfWork = UnitOfWork;
            this.hostEnvironment = hostEnvironment;
        }

        #region APIcallback

        public IActionResult AllProducts()
        {
            var products = UnitOfWork.Product.GetAll();

            return Json(new { data = products });
        }

        #endregion APIcallback

        public IActionResult Index()
        {
            ////IEnumerable<Category> categories = db.Categories;

            //ProductVM productVM = new ProductVM();
            //productVM.Products = UnitOfWork.Product.GetAll();
            //return View(productVM);
            return View();
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
            ProductVM vm = new ProductVM()
            {
                Product = new(),
                Categories = UnitOfWork.Category.GetAll().Select(X => new SelectListItem()
                {
                    Text = X.Name,
                    Value = X.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = UnitOfWork.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
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
        public IActionResult createUpdate(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = String.Empty;

                if (file != null)
                {
                    string uploadDir = Path.Combine(hostEnvironment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Product.ImageUrl = @"\ProductImage\" + fileName;
                }
                if (vm.Product.Id == 0)
                {
                    UnitOfWork.Product.Add(vm.Product);
                    TempData["success"] = "Product Created Done";
                }

                UnitOfWork.Save();
                //TempData["success"] = "Category Updated Done";

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