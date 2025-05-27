using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSaver.Data;
using PetSaver.Models;
using System.Security.Claims;

namespace PetSaver.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MapController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MapPet()
        {
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult ReportLostPet()
        {
            return View();
        }

        public IActionResult ReportFoundPet()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MapFoundPet(FoundPet foundPet)
        {
            if (foundPet == null || string.IsNullOrEmpty(foundPet.Description))
            {
                Console.WriteLine("Os dados do formulário não foram vinculados corretamente.");
                return View(foundPet); // para não quebrar a navegação
            }

            if (foundPet.ImageFile != null && foundPet.ImageFile.Length > 0)
            {
                var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(foundPet.ImageFile.FileName);

                var caminhoSalvar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/pets", nomeArquivo);

                using (var stream = new FileStream(caminhoSalvar, FileMode.Create))
                {
                    await foundPet.ImageFile.CopyToAsync(stream);
                }

                foundPet.ImageUrl = "img/pets/" + nomeArquivo;
            }

            return View(foundPet);
        }


        [HttpGet]
        public IActionResult GetSavedPets()
        {
            var pets = _context.FoundedPets.Select(pet => new
            {
                pet.Latitude,
                pet.Longitude,
                pet.Description
            }).ToList();

            return Json(pets);
        }

        [HttpPost]
        public IActionResult SaveFoundPet(FoundPet foundPet)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
              return Unauthorized(); // Retorna erro se o usuário não estiver logado
            }

            // Associa o UserId ao modelo
            foundPet.UserId = int.Parse(userId);

            if (ModelState.IsValid)
            {
                // Salva os dados no banco de dados
                _context.FoundedPets.Add(foundPet);
                _context.SaveChanges();

                // Redireciona para uma página de confirmação ou outra ação
                return RedirectToAction("Confirmation");
            }

            // Se houver erros, retorna à página atual
            return View("MapFoundPet", foundPet);
        }

        [HttpPost]
        public IActionResult SaveLostPet(LostPet lostPet)
        {
            if (ModelState.IsValid)
            {
                // Salva os dados no banco de dados
                _context.LostPets.Add(lostPet);
                _context.SaveChanges();

                // Redireciona para uma página de confirmação ou outra ação
                return RedirectToAction("MapPet");
            }

            // Se houver erros, retorna à página atual com os dados preenchidos
            return View("MapFoundPet", lostPet);
        }

    }
}
