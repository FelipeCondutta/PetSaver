using Microsoft.AspNetCore.Mvc;

namespace PetSaver.Controllers
{
    public class MapController : Controller
    {
        public IActionResult MapPet()
        {
            return View();
        }
    }
}
