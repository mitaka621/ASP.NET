using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models
{
	public class ProductViewModel
	{
        public int Id { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3,ErrorMessage ="The length must be between {2} and {1}")]
        public string Name { get; set; }=string.Empty;
        public List<string> Notes { get; set; }=new List<string>();
    }
}
