using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PetSaver.Models;
using PetSaver.Services;
using System.Threading.Tasks;

namespace PetSaver.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult LoginAndRegister()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (await _authService.Login(HttpContext, email, password))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Email ou senha inválidos.";
            return View("LoginAndRegister");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            if (await _authService.Register(username, email, password))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Erro ao registrar usuário.";
            return View("LoginAndRegister");
        }

        public IActionResult EditAccount()
        {
            // Lógica para carregar os dados da conta para edição
            return View();
        }

        public IActionResult ViewAccount()
        {
            // Lógica para carregar os dados da conta para visualização
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Remove o cookie de autenticação
            await HttpContext.SignOutAsync();

            // Redireciona para a página de login
            return RedirectToAction("Index", "Home");
        }
    }
}
