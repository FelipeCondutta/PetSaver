using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetSaver.Data;
using PetSaver.Models;
using PetSaver.Models.ViewModel;
using System.Security.Claims;

namespace PetSaver.Controllers
{
    [Authorize]
    public class PetAdoptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetAdoptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Adoption(int? idade, string raca, int? distancia, string tipo, string sexo)
        {
            var pets = _context.Pets.AsQueryable();

            if (idade.HasValue)
            {
                pets = pets.Where(p => p.Idade <= idade.Value);
            }

            if (!string.IsNullOrEmpty(raca))
            {
                pets = pets.Where(p => p.Raca.Contains(raca));
            }

            if (distancia.HasValue)
            {
                pets = pets.Where(p => p.DistanciaKm <= distancia.Value);
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                pets = pets.Where(p => p.Tipo == tipo);
            }

            if (!string.IsNullOrEmpty(sexo))
            {
                pets = pets.Where(p => p.Sexo == sexo);
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                if (tipo == "Outros")
                    pets = pets.Where(p => p.Tipo != "Cachorro" && p.Tipo != "Gato");
                else
                    pets = pets.Where(p => p.Tipo == tipo);
            }

            return View(pets.ToList());
        }

        public IActionResult MyAdoptionCases()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var requests = _context.AdoptionRequests
                .Where(r => r.UserId == userId)
                .Join(_context.Pets,
                      req => req.PetId,
                      pet => pet.Id,
                      (req, pet) => new AdoptionRequestViewModel
                      {
                          Id = req.Id,
                          PetNome = pet.Nome,
                          RequestDate = req.RequestDate,
                          IsApproved = req.IsApproved,
                          ReasonForAdoption = req.ReasonForAdoption
                      })
                .OrderByDescending(r => r.RequestDate)
                .ToList();

            return View(requests);
        }


        public IActionResult AdoptionConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAdoptionForm(AdoptionRequest adoptionRequest)
        {
            // Sempre atribua o UserId do usuário autenticado
            adoptionRequest.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.AdoptionRequests.Add(adoptionRequest);
                _context.SaveChanges();
                TempData["Mensagem"] = "Obrigado por preencher o formulário! Entraremos em contato em breve.";
                return RedirectToAction("AdoptionConfirmation");
            }
            // Se houver erro, retorna para o formulário com os dados preenchidos
            return View("AdoptionForm", adoptionRequest);
        }



        public IActionResult AdoptionForm(int id)
        {
            var model = new AdoptionRequest { PetId = id };
            return View(model);
        }

    }
}
