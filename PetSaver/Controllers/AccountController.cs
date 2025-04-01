using Microsoft.AspNetCore.Mvc;

namespace PetSaver.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LoginAndRegister()
        {
            return View();
        }
    }
}
