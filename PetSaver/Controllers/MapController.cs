using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSaver.Data;
using PetSaver.Models;
using PetSaver.Models.ViewModel;
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
            // Obtenha os pets encontrados do banco de dados
            var foundPets = _context.FoundedPets
                .Select(pet => new
                {
                    pet.Id,
                    pet.Description,
                    pet.Latitude,
                    pet.Longitude,
                    pet.ImageUrl
                })
                .ToList();

            // Passe os dados para a View como JSON
            ViewData["FoundPets"] = System.Text.Json.JsonSerializer.Serialize(foundPets);

            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult ConfirmationLost()
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

        [HttpGet]
        public IActionResult UserPetCases()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
                return Unauthorized();

            int userId = int.Parse(userIdStr);

            // Busca pets perdidos
            // Busca pets perdidos
            var lostCases = _context.LostPets
                .Where(p => p.UserId == userId)
                .ToList() // Materializa aqui!
                .Select(p => new UserPetCaseViewModel
                {
                    IsLost = true,
                    Description = p.Description,
                    ApproximateAge = p.ApproximateAge,
                    AgeUnit = p.AgeUnit,
                    Gender = p.Gender,

                });

            // Busca pets encontrados
            var foundCases = _context.FoundedPets
                .Where(p => p.UserId == userId)
                .ToList() // Materializa aqui!
                .Select(p => new UserPetCaseViewModel
                {
                    IsLost = false,
                    Description = p.Description,
                    ApproximateAge = p.ApproximateAge,
                    AgeUnit = p.AgeUnit,
                    Gender = p.Gender,
                });

            var allCases = lostCases
                .Concat(foundCases)
                .OrderByDescending(c => c.Date)
                .ToList();

            return View(allCases);


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

        [HttpPost]
        public async Task<IActionResult> MapLostPet(LostPet lostpet)
        {
            if (lostpet == null || string.IsNullOrEmpty(lostpet.Description))
            {
                Console.WriteLine("Os dados do formulário não foram vinculados corretamente.");
                return View(lostpet); // para não quebrar a navegação
            }

            if (lostpet.ImageFile != null && lostpet.ImageFile.Length > 0)
            {
                var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(lostpet.ImageFile.FileName);

                var caminhoSalvar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/pets", nomeArquivo);

                using (var stream = new FileStream(caminhoSalvar, FileMode.Create))
                {
                    await lostpet.ImageFile.CopyToAsync(stream);
                }

                lostpet.ImageUrl = "img/pets/" + nomeArquivo;
            }

            return View(lostpet);
        }

        [HttpGet]
        public IActionResult GetSavedPets()
        {
            var pets = _context.FoundedPets.Select(pet => new
            {
                pet.Latitude,
                pet.Longitude,
                pet.Description,
                pet.ImageUrl 
            }).ToList();

            return Json(pets);
        }

        [HttpPost]
        public IActionResult SaveFoundPet(FoundPet foundPet)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
              return Unauthorized();
            }

            // Associa o UserId ao modelo
            foundPet.UserId = int.Parse(userId);

            if (ModelState.IsValid)
            {
                _context.FoundedPets.Add(foundPet);
                _context.SaveChanges();

                return RedirectToAction("Confirmation");
            }

            return View("MapFoundPet", foundPet);
        }

        [HttpPost]
        public IActionResult SaveLostPet(LostPet lostpet)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            // Associa o UserId ao modelo
            lostpet.UserId = int.Parse(userId);

            if (ModelState.IsValid)
            {
                // Salva os dados no banco de dados
                _context.LostPets.Add(lostpet);
                _context.SaveChanges();

                // Redireciona para uma página de confirmação ou outra ação
                return RedirectToAction("ConfirmationLost");
            }

            // Se houver erros, retorna à página atual
            return View("MapLostPet", lostpet);
        }

    }
}
