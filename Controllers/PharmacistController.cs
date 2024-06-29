using Microsoft.AspNetCore.Mvc;
using MCS.Models;
using System.Linq;
using MCS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MCS.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly McsContext _context;
        private readonly SignInManager<DeptStaff> _signInManager;

        public PharmacistController(McsContext context, SignInManager<DeptStaff> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userName = TempData["UserName"] as string;
            ViewBag.UserName = userName;
            return View();
        }

        [HttpGet]
        public IActionResult SearchPatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchPatient(long patientId)
        {
            if (patientId <= 0)
            {
                return BadRequest("Invalid patient ID.");
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            var prescriptions = await _context.Prescriptions
            .Where(p => p.PatientId == patientId)
            .Include(p => p.Medications)
            .ToListAsync();

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
            var model = new UpdateStorageViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStorage(UpdateStorageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pharmacy = new Pharmacy
                {
                    MedicineName = model.MedicineName,
                    Stock = model.Stock,
                    Shelf = model.Shelf
                };

                _context.Pharmacies.Add(pharmacy);
                _context.SaveChanges();

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var pharmacy = await _context.Pharmacies.FindAsync(id);
            if (pharmacy != null)
            {
                _context.Pharmacies.Remove(pharmacy);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(UpdateStorage));
        }
        [HttpGet]
        public IActionResult MedicineLockup()
        {
            var model = new MedicineLookupViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MedicineLockup(MedicineLookupViewModel model)
        {
            if (!string.IsNullOrEmpty(model.MedicineName))
            {
                model.SearchResults = await _context.Pharmacies
                    .Where(m => m.MedicineName.Contains(model.MedicineName))
                    .Select(m => new MedicineViewModel
                    {
                        MedicineName = m.MedicineName,
                        Stock = m.Stock,
                        Shelf = m.Shelf.ToString()
                    }).ToListAsync();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string medicineName, long stock, long shelf)
        {
            var medicine = _context.Pharmacies.FirstOrDefault(m => m.MedicineName == medicineName);
            if (medicine != null)
            {
                medicine.Stock = stock;
                medicine.Shelf = shelf;
                _context.SaveChanges();
            }

            return RedirectToAction("MedicineLockup");
        }

    }
}
