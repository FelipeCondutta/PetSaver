using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSaver.Data;
using PetSaver.Models;
using PetSaver.Models.ViewModel;
using PetSaver.Services;
using System.Threading.Tasks;

namespace PetSaver.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly ApplicationDbContext _context;
        public AccountController(AuthService authService, ApplicationDbContext context)
        {
            _authService = authService;
            _context = context;
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

        [HttpGet]
        public IActionResult EditProfile()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return NotFound();

            var vm = new EditProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dbUser = _context.Users.FirstOrDefault(u => u.Id == model.Id);
            if (dbUser == null)
                return NotFound();

            // Verifique a senha (ajuste se usar hash)
            if (dbUser.Password != model.CurrentPassword)
            {
                ViewBag.ErrorMessage = "Senha incorreta.";
                return View(model);
            }

            dbUser.Name = model.Name;
            dbUser.Email = model.Email;
            _context.SaveChanges();

            return RedirectToAction("Profile");
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

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var vm = new ChangePasswordViewModel { Id = userId };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dbUser = _context.Users.FirstOrDefault(u => u.Id == model.Id);
            if (dbUser == null)
                return NotFound();

            if (dbUser.Password != model.CurrentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "A senha atual está incorreta. Por favor, tente novamente.");
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "A nova senha e a confirmação não coincidem.");
                return View(model);
            }

            dbUser.Password = model.NewPassword; // Considere hashear a senha!
            _context.SaveChanges();

            return RedirectToAction("Profile");
        }



    }
}
