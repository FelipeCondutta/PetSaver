using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSaver.Data;
using PetSaver.Models;

namespace PetSaver.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Exemplo de método no HomeController
        public IActionResult Index()
        {
            var petsParaAdocao = _context.Pets.Take(4).ToList(); // Pegue os 4 primeiros, por exemplo
            ViewBag.PetsParaAdocao = petsParaAdocao;
            return View();
        }

    }



}
