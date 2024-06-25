using Microsoft.AspNetCore.Mvc;
using MCS.Entities;
using MCS.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Controllers
{
    public class DoctorController : Controller
    {
        private readonly McsContext _context;

        public DoctorController(McsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ViewPatient()
        {
            var userName = TempData["UserName"] as string;
            ViewBag.UserName = userName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewPatient(string viewPatients)
        {
            if (string.IsNullOrEmpty(viewPatients))
            {
                ViewData["ErrorMessage"] = "Please select an option.";
                return View();
            }
            switch (viewPatients)
            {
                case "Diagnosis":
                    return RedirectToAction("Diagnosis");
                case "LabTests":
                    return RedirectToAction("LabTests");
                case "Radiology":
                    return RedirectToAction("Radiology");
                case "Prescription":
                    return RedirectToAction("Prescription");
                default:
                    return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderLabTest(TestViewModel testViewModel)
        {
            if (ModelState.IsValid)
            {
                Test test = new Test
                {
                    PatientId = testViewModel.PatientId,
                    TestType = testViewModel.TestType,
                    TestDate = testViewModel.TestDate,
                    PatientName = testViewModel.PatientName,
                    DoctorId = null, // Set as needed
                    RecievedResult = null, // Set as needed
                    Results = null, // Set as needed
                    Technician = null, // Set as needed
                    NormalRange = null // Set as needed
                };

                _context.Tests.Add(test);
                _context.SaveChanges();

                // Redirect to a confirmation page or the list of tests
                return RedirectToAction("Index");
            }

            // If model state is invalid, return the view with the same model to show validation errors
            return View(testViewModel);
        }

        [HttpGet]
        public IActionResult OrderLabTest()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OrderRadiology()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderRadiology(RadiologyViewModel testViewModel)
        {
            if (ModelState.IsValid)
            {
                Radiology obj = new Radiology
                {
                    DoctorId = null,
                    ImageDate = testViewModel.ImageDate.Date,
                    ImagePath = null,
                    PatientId = testViewModel.PatientId,
                    ImageType = testViewModel.ImageType,
                    TechnicianName = null,
                    TechnicianNotes = null,
                    PatientName = testViewModel.PatientName
                };

                _context.Radiologies.Add(obj);
                _context.SaveChanges();

                // Redirect to a confirmation page or the list of tests
                return RedirectToAction("Index");
            }

            // If model state is invalid, return the view with the same model to show validation errors
            return View(testViewModel);
        }

        [HttpGet]
        public IActionResult EnterDiagnosis()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterDiagnosis(EnterDiagnosesModel diagnosesModel)
        {
            if (ModelState.IsValid)
            {
                Diagnosis diagnosis = new Diagnosis
                {
                    DoctorId = diagnosesModel.DoctorID,
                    Diagnosis1 = diagnosesModel.Diagnosed,
                    DiagnosisDate = diagnosesModel.DiagnoseDate,
                    PatientId = diagnosesModel.PatientID,
                    PatientName = diagnosesModel.PatientName
                };

                _context.Diagnoses.Add(diagnosis);
                _context.SaveChanges();

                // Redirect to a confirmation page or the list of tests
                return RedirectToAction("Index");
            }

            // If model state is invalid, return the view with the same model to show validation errors
            return View(diagnosesModel);
        }

        [HttpGet]
        public IActionResult PrescribeMedicine()
        {
            var model = new PrescriptionViewModel();
            model.Medications.Add(new MedicationViewModel()); // Initialize with one medication
            return View(model);
        }

        [HttpPost]
        public IActionResult PrescribeMedicine(PrescriptionViewModel prescriptionModel)
        {
            if (ModelState.IsValid)
            {
                Prescription prescription = new Prescription
                {
                    PatientId = prescriptionModel.PatientId,
                    PatientName = prescriptionModel.PatientName,
                    Comments = prescriptionModel.Comments,
                    CreatedDate = DateTime.Now,
                    Medications = prescriptionModel.Medications.Select(m => new Medication
                    {
                        MedicineName = m.MedicineName,
                        Dosage = m.Dosage,
                        Duration = m.Duration
                    }).ToList()
                };

                _context.Prescriptions.Add(prescription);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(prescriptionModel);
        }

        [HttpGet]
        public IActionResult Diagnosis()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Diagnosis(long patientId)
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

            var diagnoses = _context.Diagnoses.Where(d => d.PatientId == patientId).ToList();

            var model = new DiagnosisModel
            {
                PatientId = patient.Id,
                PatientName = patient.Name,
                Diagnoses = diagnoses
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult LabTests()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LabTests(long patientId)
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

            var labTests = _context.Tests.Where(t => t.PatientId == patientId).ToList();

            var model = new LabTestModel
            {
                PatientId = patient.Id,
                PatientName = patient.Name,
                LabTests = labTests
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Radiology()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Radiology(long patientId)
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

            var radiologies = _context.Radiologies.Where(r => r.PatientId == patientId).ToList();

            var model = new RadiologyModel
            {
                PatientId = patient.Id,
                PatientName = patient.Name,
                Radiologies = radiologies
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Prescription()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prescription(long patientId)
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

    }
}
