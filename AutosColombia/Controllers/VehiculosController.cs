
using Microsoft.AspNetCore.Mvc;

namespace AutosColombia.Controllers
{
    public class VehiculosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
