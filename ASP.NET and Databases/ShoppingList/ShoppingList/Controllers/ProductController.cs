using Microsoft.AspNetCore.Mvc;
using ShoppingList.Contracts;
using ShoppingList.Models;

namespace ShoppingList.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;

        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            return View(products);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel product)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            await productService.AddProductAsync(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            return View(await productService.GetProductByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel product)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            await productService.UpdateProductAsync(product);
            return RedirectToAction("Index");
        }

		[HttpGet]
		public IActionResult AddNote(int id)
		{
            ViewBag.Id = id;
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> AddNote([FromForm] int id, [FromForm] string content)
        {
           await productService.AddNoteToPRoduct(id,content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
           await productService.DeleteProductAsync(id);

            return RedirectToAction("Index");
        }
	}
}
