using _1.Create_Simple_Pages_in_an_ASP.NET_Core_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _1.Create_Simple_Pages_in_an_ASP.NET_Core_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "new title";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["h2Content"] = "This is an ASP.NET Core MVC app.";
            ViewBag.pContent = "This is the Aboput page for fvhjsdgfjkyhdsz";
            return View();
        }

		public IActionResult Numbers()
		{
			return View();
		}

		public IActionResult NumberstoN(int count=3)
		{
            ViewBag.count = count;
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
