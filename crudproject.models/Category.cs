using System.ComponentModel.DataAnnotations;

namespace crudproject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [Display(Name = "Display Order")]
        public int Displayorder { get; set; } = default!;

        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}