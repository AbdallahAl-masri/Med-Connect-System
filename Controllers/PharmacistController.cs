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

        [HttpGet]
        public IActionResult PLogin()
        {
            return View(new StaffLoginModel());
        }

        [HttpPost("PLogin")]
        public async Task<IActionResult> PLogin(StaffLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var staff = await _context.DeptStaffs
                    .FirstOrDefaultAsync(s => s.StaffId == model.StaffID && s.DepartmentId == model.DepartmentID);

                if (staff == null)
                {
                    return BadRequest("Invalid staff ID or department.");
                }

                var result = _context.DeptStaffs.Where(p => p.PasswordHash == model.Password);

                if (result != null)
                {
                    // Check role and redirect accordingly
                    if (staff.Role == "Doctor")
                    {
                        return RedirectToAction("Index", "Doctor");
                    }
                    else if (staff.Role == "Pharmacist")
                    {
                        return RedirectToAction("Index", "Pharmacist");
                    }
                    else if (staff.Role == "Lab Technician")
                    {
                        return RedirectToAction("Index", "LabTechnician");
                    }
                    else if (staff.Role == "Radiology Technician")
                    {
                        return RedirectToAction("Index", "RadiologyTechnician");
                    }
                    else if (staff.Role == "Staff")
                    {
                        return RedirectToAction("index", "Medical_Personnel");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we reach here, something went wrong; return the view with errors
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("PLogin", "Pharmacist");
        }
    }
}
