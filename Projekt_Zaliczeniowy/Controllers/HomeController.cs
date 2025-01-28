using Microsoft.AspNetCore.Mvc;

namespace Projekt_Zaliczeniowy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
