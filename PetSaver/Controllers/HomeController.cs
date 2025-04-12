using Microsoft.AspNetCore.Mvc;
using PetSaver.Data;
using PetSaver.Models;

namespace PetSaver.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public IActionResult Index()
        {
            return View(); // Retorna a View "Index.cshtml" dentro de Views/Home/
        }
    }



}
