using CLDV_POE_1_Example_Movie_Database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CLDV_POE_1_Example_Movie_Database.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
