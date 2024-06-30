using MCS.Entities;
using MCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace MCS.Controllers
{
    public class Medical_Personnel : Controller
    {
        private readonly McsContext _context;

        public Medical_Personnel(McsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PatientProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PatientProfile(string PatientProfile)
        {
            switch (PatientProfile)
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
            var userName = TempData["UserName"] as string;
            ViewBag.UserName = userName;
            return View();
        }

        public async Task<IActionResult> ReserveAppointment()
        {
            var model = new ReserveAppointmentViewModel
            {
                Departments = await _context.Departments.ToListAsync()
            };
            return View(model);
        }

        // POST: ReserveAppointment
        [HttpPost]
        public async Task<IActionResult> ReserveAppointment(ReserveAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Name == model.Department);
                // Here, you would save the appointment to the database
                Appointment newAppointment = new Appointment
                {
                    DepartmentId = dept.Id,
                    PatientId = model.PatientId,
                    Timeslot = model.AppointmentTime,
                    Datetime = model.AppointmentDate.Date,
                    Status = "Scheduled",
                    DoctorId = model.DoctorId,

                };

                _context.Appointments.Add(newAppointment);
                _context.SaveChanges();

                // Redirect to a success page or return a view
                return RedirectToAction("Index", "Medical_Personnel"); // Redirect to home page for now
            }

            // If model state is not valid, return to the view with errors
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorsByDepartment(string departmentName)
        {
            var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Name == departmentName);
            var doctors = await _context.Doctors
                .Where(e => e.ClinicId == dept.Id)
                .Select(e => new { id = e.Id, name = e.Name })
                .ToListAsync();

            return Json(doctors);
        }

    [HttpGet]
        public async Task<ActionResult> ManageAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Department)
                .Include(a => a.Doctor)
                .Select(a => new AppointmentViewModel
                {
                    Id = a.Id,
                    PatientId = a.PatientId,
                    Department = a.Department.Name,
                    AppointmentTime = a.Timeslot,
                    Doctor = a.Doctor.Name,
                    Status = a.Status,
                    AppointmentDate = a.Datetime,
                })
                .ToListAsync();

            return View(appointments);
        }


        // POST: Appointments/ManageAppointments
        [HttpPost]
        public async Task<ActionResult> ManageAppointments(List<AppointmentViewModel> appointments)
        {
            if (ModelState.IsValid)
            {
                foreach (var viewModel in appointments)
                {
                    var appointment = await _context.Appointments
                        .Include(a => a.Department)
                        .Include(a => a.Doctor)
                        .FirstOrDefaultAsync(a => a.Id == viewModel.Id);

                    if (appointment != null)
                    {
                        appointment.PatientId = viewModel.PatientId;
                        appointment.Timeslot = viewModel.AppointmentTime;
                        appointment.Status = viewModel.Status;
                        appointment.Datetime = viewModel.AppointmentDate;

                        var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == viewModel.Department);
                        if (department != null)
                        {
                            appointment.DepartmentId = department.Id;
                        }

                        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Name == viewModel.Doctor);
                        if (doctor != null)
                        {
                            appointment.DoctorId = doctor.Id;
                        }

                        _context.Appointments.Update(appointment);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageAppointments");
            }
            return View(appointments);
        }

        [HttpGet]
        public IActionResult Diagnosis()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Diagnosis(long patientId)
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

            var diagnoses = await _context.Diagnoses.Where(d => d.PatientId == patientId).ToListAsync();

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
        public async Task<IActionResult> LabTests(long patientId)
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

            var labTests = await _context.Tests.Where(t => t.PatientId == patientId).ToListAsync();

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
        public async Task<IActionResult> Radiology(long patientId)
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

            var radiologies = await _context.Radiologies.Where(r => r.PatientId == patientId).ToListAsync();

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
        public async Task<IActionResult> Prescription(long patientId)
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
        public async Task<IActionResult> Edit(long id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Department)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            var viewModel = new AppointmentViewModel
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                Department = appointment.Department.Name,
                AppointmentTime = appointment.Timeslot,
                AppointmentDate = appointment.Datetime,
                Status = appointment.Status,
                Doctor = appointment.Doctor.Name,
            };

            return View(viewModel);
        }


        // POST: Appointments/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appointment = await _context.Appointments
                    .Include(a => a.Department)
                    .Include(a => a.Doctor)
                    .FirstOrDefaultAsync(p => p.Id == viewModel.Id);

                if (appointment == null)
                {
                    return NotFound();
                }

                appointment.PatientId = viewModel.PatientId;
                appointment.Timeslot = viewModel.AppointmentTime;
                appointment.Status = viewModel.Status;
                appointment.Datetime = viewModel.AppointmentDate;

                // Update the foreign key values
                var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == viewModel.Department);
                if (department != null)
                {
                    appointment.DepartmentId = department.Id;
                }

                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Name == viewModel.Doctor);
                if (doctor != null)
                {
                    appointment.DoctorId = doctor.Id;
                }

                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                return RedirectToAction("ManageAppointments");
            }

            return View(viewModel);
        }


        // POST: Appointments/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return RedirectToAction("ManageAppointments");
        }
    }
}
