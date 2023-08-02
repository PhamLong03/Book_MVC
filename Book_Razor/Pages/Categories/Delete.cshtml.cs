using Book_Razor.Data;
using Book_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Book_Razor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet(int? id)
        {
            if (id != null && id != 0) 
            {
                Category = _db.Categories.Find(id);
                _db.Categories.Remove(Category);
                _db.SaveChanges();  
            }
            return RedirectToPage("Index");
        }
    }
}
