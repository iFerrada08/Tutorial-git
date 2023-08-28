using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDeveloper.Mvc.Controllers
{
    public class DemoController : Controller
    {
        // GET: /<controller>/
        // GET: /<controller>/index
        public string Index()
        {
            return "Hola Mundo";
        }

        [Route("Demo/Bienvenido/{nombre}")]
        public IActionResult Bienvenido(string nombre)
        {
            ViewData["nombre"] = nombre;
            ViewBag.Nombre = nombre;
            var arreglo = new string[] { "uno", "dos" };
            ViewData["arreglo"] = arreglo;
            return View();
        }

        public IActionResult BienvenidoVista()
        {
            return View("Bienvenido");
        }
    }
}
