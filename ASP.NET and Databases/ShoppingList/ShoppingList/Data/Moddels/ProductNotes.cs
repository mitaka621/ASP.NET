using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Data.Moddels
{
	public class ProductNotes
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(500)]
		public string Content { get; set; } = null!;

		[Required]
		public int ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }=null!;
		
    }
}
