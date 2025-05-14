using Microsoft.AspNetCore.Mvc;
using PetSaver.Data;
using PetSaver.Models;

namespace PetSaver.Controllers
{
    public class HomeController : Controller
    {  
        public IActionResult Index()
        {
            return View();
        }
    }



}
