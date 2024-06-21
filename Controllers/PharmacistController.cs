using Microsoft.AspNetCore.Mvc;
using MCS.Models;
using System.Linq;
using MCS.Entities;
using Microsoft.EntityFrameworkCore;

namespace MCS.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly McsContext _context;

        public PharmacistController(McsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchPatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchPatient(long patientId)
        {
            if (patientId <= 0)
            {
                return BadRequest("Invalid patient ID.");
            }

            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            var prescriptions = _context.Prescriptions
            .Where(p => p.PatientId == patientId)
            .Include(p => p.Medications)
            .ToList();

            var model = new PrescriptionModel
            {
                PatientId = patient.Id,
                PatientName = patient.Name,
                Prescriptions = prescriptions
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateStorage()
        {
            var model = new UpdateStorageViewModel
            {
                ExistingStorage = _context.Pharmacies.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateStorage(UpdateStorageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pharmacy = new Pharmacy
                {
                    MedicineId = model.MedicineId,
                    MedicineName = model.MedicineName,
                    Stock = model.Stock,
                    Shelf = model.Shelf
                };

                _context.Pharmacies.Add(pharmacy);
                _context.SaveChanges();

                // Refresh the list
                model.ExistingStorage = _context.Pharmacies.ToList();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var pharmacy = _context.Pharmacies.Find(id);
            if (pharmacy != null)
            {
                _context.Pharmacies.Remove(pharmacy);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(UpdateStorage));
        }

        public IActionResult MedicineLockup()
        {
            var model = new MedicineLookupViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult MedicineLockup(MedicineLookupViewModel model)
        {
            if (!string.IsNullOrEmpty(model.MedicineName))
            {
                model.SearchResults = _context.Pharmacies
                    .Where(m => m.MedicineName.Contains(model.MedicineName))
                    .Select(m => new MedicineViewModel
                    {
                        MedicineName = m.MedicineName,
                        Stock = m.Stock,
                        Shelf = m.Shelf.ToString()
                    }).ToList();
            }

            return View(model);
        }
    }
}
