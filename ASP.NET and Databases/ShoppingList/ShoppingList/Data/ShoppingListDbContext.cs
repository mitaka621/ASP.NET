using Microsoft.EntityFrameworkCore;
using ShoppingList.Data.Moddels;

namespace ShoppingList.Data
{
	public class ShoppingListDbContext:DbContext
	{
        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options):base(options)
        {
                
        }
       public DbSet<Product> Products { get; set; }
        public DbSet<ProductNotes> ProductNotes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasData(new Product()
				{
					Id = 1,
					Name = "Milk"
				},
				new Product()
				{
					Id = 2,
					Name = "Lutenica"
				});

			modelBuilder.Entity<ProductNotes>()
				.HasData(new ProductNotes()
				{
					Id = 1,
					Content = "Trqq da se kupi ot kauflanda",
					ProductId = 1,
				});
		}
	}
}
