using Microsoft.AspNetCore.Mvc;

namespace PetSaver.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Retorna a View "Index.cshtml" dentro de Views/Home/
        }
    }



}
