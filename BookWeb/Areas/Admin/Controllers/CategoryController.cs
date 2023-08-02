using Book.DataAccess.Repository.IReporistory;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnit _unit;
        public CategoryController(IUnit unit)
        {
            _unit = unit;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unit.UnitCategory.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category name and Display order can not be match");
            }
            if (ModelState.IsValid)
            {
                _unit.UnitCategory.Add(obj);
                _unit.Save();
                TempData["success"] = "Create successfully";
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _unit.UnitCategory.GetFirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category name and Display order can not be match");
            }
            if (ModelState.IsValid)
            {
                _unit.UnitCategory.Update(obj);
                _unit.Save();
                TempData["success"] = "Edit successfully";
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _unit.UnitCategory.GetFirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unit.UnitCategory.Delete(category);
            _unit.Save();
            TempData["success"] = "Delete successfully";
            return RedirectToAction("Index");
        }
    }
}
