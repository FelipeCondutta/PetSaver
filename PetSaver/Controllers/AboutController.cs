using Microsoft.AspNetCore.Mvc;

namespace PetSaver.Controllers
{
    public class AboutController: Controller
    {
        [HttpGet]
        public IActionResult AboutNewOwner()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DogAge()
        {
            return View();
        }
    }
}
