using Microsoft.AspNetCore.Mvc;
using PetSaver.Data;
using PetSaver.Models;

namespace PetSaver.Controllers
{
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

        public IActionResult ReportLostPet()
        {
            return View();
        }

        public IActionResult ReportFoundPet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MapFoundPet(FoundPet foundPet)
        {
            // Armazena os dados do formulário na TempData para uso posterior
            TempData["FoundPet"] = System.Text.Json.JsonSerializer.Serialize(foundPet);

            // Renderiza a página MapFoundPet
            return View();
        }

        [HttpPost]
        public IActionResult SaveFoundPet()
        {
            // Recupera os dados do TempData
            var foundPetJson = TempData["FoundPet"] as string;
            if (foundPetJson == null)
            {
                return RedirectToAction("ReportFoundPet");
            }

            var foundPet = System.Text.Json.JsonSerializer.Deserialize<FoundPet>(foundPetJson);

            if (ModelState.IsValid)
            {
                // Salva os dados no banco de dados
                _context.FoundedPets.Add(foundPet);
                _context.SaveChanges();

                // Redireciona para uma página de confirmação ou outra ação
                return RedirectToAction("MapPet");
            }

            // Se houver erros, retorna à página atual
            return View("MapFoundPet");
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
