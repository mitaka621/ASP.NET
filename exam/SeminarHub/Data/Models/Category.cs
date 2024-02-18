using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Data.Models
{
    [Comment("Seminar category")]
    public class Category
    {
        [Key]
        [Comment("Unique identifier for the category")]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Category.MaxNameLength)]
        [Comment("Category name")]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}