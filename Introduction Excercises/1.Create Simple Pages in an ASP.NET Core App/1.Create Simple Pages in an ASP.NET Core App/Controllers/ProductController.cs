using _1.Create_Simple_Pages_in_an_ASP.NET_Core_App.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;

namespace _1.Create_Simple_Pages_in_an_ASP.NET_Core_App.Controllers
{
	public class ProductController : Controller
	{
        IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name="sirini",
                Price=2.99
            },
            new ProductViewModel()
            {
                Id = 2,
                Name="turshiq",
                Price=2
            },
            new ProductViewModel()
            {
                Id = 3,
                Name="lutinica",
                Price=11
            },
            new ProductViewModel()
            {
                Id = 4,
                Name="krastavi4ka",
                Price=0.23
            }
        };

        private readonly ILogger<HomeController> _logger;

        public ProductController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult All()
		{
			return View(products);
		}

		public IActionResult ById(int id)
		{
            var currentProduct = products.FirstOrDefault(x => x.Id == id);

			return currentProduct == null ? View("All",products) : View(currentProduct);			
		}

		public IActionResult AllAsJSON()
		{
			return Json(products);
		}

		public IActionResult AllAsPlainText()
		{
            StringBuilder sb=new StringBuilder();
            foreach (var product in products)
            {
                sb.Append($"Product {product.Id}: {product.Name} - {product.Price}lv{Environment.NewLine}");
            }
			return Content(sb.ToString().TrimEnd());
		}
		public IActionResult AllAsText()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var product in products)
			{
				sb.Append($"Product {product.Id}: {product.Name} - {product.Price}lv{Environment.NewLine}");
			}
            Response.Headers.Append(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");
            
			return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()),"text/plain");
		}
	}
}
