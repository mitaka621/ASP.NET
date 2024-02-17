using ShoppingList.Models;

namespace ShoppingList.Contracts
{
	public interface IProductService
	{
		Task<IEnumerable<ProductViewModel>> GetAllAsync();

		Task AddProductAsync(ProductViewModel product);

		Task UpdateProductAsync(ProductViewModel product);

		Task DeleteProductAsync(int id);

		Task<ProductViewModel> GetProductByIdAsync(int id);

		Task AddNoteToPRoduct(int id, string content);
	}
}
