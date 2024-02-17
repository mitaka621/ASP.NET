using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Data.Moddels
{
	public class Product
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }=string.Empty;

        public List<ProductNotes> ProductNotes { get; set; }=new List<ProductNotes>();
    }


}
