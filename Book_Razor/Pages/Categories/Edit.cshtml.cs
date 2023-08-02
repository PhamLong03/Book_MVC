using Book_Razor.Data;
using Book_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Book_Razor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public Category Category { get; set; }  
        public EditModel(ApplicationDbContext db)
        {
            _db = db; 
        }

        public void OnGet(int? id)
        {
            if(id != null && id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }
        public IActionResult OnPost() 
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
