using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
 
namespace Book_Razor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Invalid value!")]
        [DisplayName("Category name")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Invalid value!")]
        [DisplayName("Display order")]
        public int DisplayOrder { get; set; }
    }
}
