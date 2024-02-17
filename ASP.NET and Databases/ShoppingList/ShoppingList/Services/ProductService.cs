using Azure;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Contracts;
using ShoppingList.Data;
using ShoppingList.Data.Moddels;
using ShoppingList.Models;

namespace ShoppingList.Services
{
	public class ProductService : IProductService
	{
		private readonly ShoppingListDbContext context;

		public ProductService(ShoppingListDbContext _context)
		{
			context = _context;
		}

		public async Task AddNoteToPRoduct(int id, string content)
		{
			var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

			if (product == null)
			{
				throw new ArgumentException("Invalid Id");
			}

			product.ProductNotes.Add(new ProductNotes() { Content = content });

			await context.SaveChangesAsync();
		}

		public async Task AddProductAsync(ProductViewModel product)
		{
			await context.Products.AddAsync(new Product() { Name = product.Name });
			await context.SaveChangesAsync();
		}

		public async Task DeleteProductAsync(int id)
		{
			var products = await context.Products.Include(x=>x.ProductNotes).FirstAsync(x => x.Id == id);
			context.Products.Remove(products);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
		{
			return await context.Products
				.AsNoTracking()
				.Select(x => new ProductViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					Notes = x.ProductNotes.Select(x => x.Content).ToList(),
				}).ToListAsync();
		}

		public async Task<ProductViewModel> GetProductByIdAsync(int id)
		{
			var product = await context.Products
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
			if (product == null)
			{
				throw new ArgumentException("Invalid Id");
			}
			return new ProductViewModel()
			{
				Id = product.Id,
				Name = product.Name,
			};
		}

		public async Task UpdateProductAsync(ProductViewModel product)
		{

			var entity = await context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
			if (entity == null)
			{
				throw new ArgumentException("Invalid Id");
			}
			entity.Name = product.Name;
			context.SaveChanges();
		}
	}
}
