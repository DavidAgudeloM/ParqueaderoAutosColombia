using Microsoft.AspNetCore.Mvc;

namespace AutosColombia.Controllers
{
    public class ParqueoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
