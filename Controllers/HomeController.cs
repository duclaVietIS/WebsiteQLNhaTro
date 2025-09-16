using Microsoft.AspNetCore.Mvc;

namespace WebsiteQLNhaTro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Home.cshtml");
        }
    }
}
