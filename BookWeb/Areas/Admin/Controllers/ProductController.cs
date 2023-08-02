using Book.DataAccess.Repository.IReporistory;
using Book.Models;
using Book.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechTalk.SpecFlow.CommonModels;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnit _unit;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnit unit, IWebHostEnvironment webHostEnvironment)
        {
            _unit = unit;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product>  products= _unit.UnitProduct.GetAll(includes:"Category").ToList();
            
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new(){
                CategoryList = _unit.UnitCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
            };
            if(id==null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unit.UnitProduct.GetFirstOrDefault(u=>u.Id == id);
                return View(productVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRoot, @"Img\Product");
                    if (!string.IsNullOrEmpty(obj.Product.ImgUrl))
                    {
                        var oldImgPath = Path.Combine(wwwRoot, obj.Product.ImgUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }
                    }
                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImgUrl = @"\Img\Product\" + fileName;
                }

                if (obj.Product.Id==0)
                {
                    _unit.UnitProduct.Add(obj.Product);
                   
                }
                else
                {
                    _unit.UnitProduct.Update(obj.Product);
                }
                _unit.Save();
                TempData["success"] = "Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ProductVM productVM = new(){
                CategoryList = _unit.UnitCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
                };
            return View(productVM);
            }
        }

/*        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _unit.UnitProduct.GetFirstOrDefault(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unit.UnitProduct.Delete(product);
            _unit.Save();
            TempData["success"] = "Delete successfully";
            return RedirectToAction("Index");
        }*/
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Product> products = _unit.UnitProduct.GetAll(includes: "Category").ToList();
            return Json(new {data = products});
        }

        public IActionResult Delete(int id)
        {
            var productToBeDeleted = _unit.UnitProduct.GetFirstOrDefault(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath,
                           productToBeDeleted.ImgUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unit.UnitProduct.Delete(productToBeDeleted);
            _unit.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}
