using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutosColombia.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ingresar(string usuario, string clave)
        {
            if (usuario == "admin" && clave == "1234")
            {
                HttpContext.Session.SetString("usuario", usuario);
                return RedirectToAction("Index", "Vehiculos");
            }

            ViewBag.Error = "Datos incorrectos";
            return View("Index");
        }

        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
