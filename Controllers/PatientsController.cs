using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCS.Data;
using MCS.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MCS.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
      
        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        private bool PatientExists(int id)
        {
          return (_context.Patient?.Any(e => e.ID == id)).GetValueOrDefault();
        }
        // GET: api/Patient/PatientAppointments/{patientId}
        [HttpGet("GetPatientAppointments/{patientId}")]
        public ActionResult<IEnumerable<PatientAppointments>> GetPatientAppointments(int patientId)
        {
            // Retrieve appointments for the specified patient and structure them into a list
            var appointments = _context.PatientAppointment
                .Where(a => a.PatientID == patientId)
                .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No appointments found for the specified patient.");
            }
            // will return the appointments as a list where we will parse them as json in the mobile app and display them
            return Ok(appointments);
        }

        // GET: api/Patient/PatientRadiology/{patientId}
        [HttpGet("PatientRadiology/{patientId}")]
        public ActionResult<IEnumerable<PatientRadiology>> GetPatientRadiology(int patientId)
        {
            // Retrieve images for the specified patient and structure them into a list
            var radiology = _context.PatientRadiology
                .Where(r => r.PatientID == patientId)
                .ToList();

            if (radiology == null || radiology.Count == 0)
            {
                return NotFound("No images found for the specified patient.");
            }
            // will return the images as a list where we will parse them as json in the mobile app and display them
            return Ok(radiology);
        }
        [HttpGet("GetpatientTests/{patientId}")]
        public ActionResult<IEnumerable<Test>> GetPatientTests(int patientId)
        {
            // Retrieve images for the specified patient and structure them into a list
            var tests = _context.Test
                .Where(t => t.PatientID == patientId)
                .ToList();

            if (tests == null || tests.Count == 0)
            {
                return NotFound("No images found for the specified patient.");
            }
            // will return the images as a list where we will parse them as json in the mobile app and display them
            return Ok(tests);
        }
        //set email
        [HttpPut("SetPatientEmail/{patientId}")]
        public async Task<IActionResult> SetPatientEmail(int patientId, [FromForm] string email)
        {
            string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            // Validate email format
            if (!Regex.IsMatch(email, emailPattern))
            {
                return BadRequest("Invalid email format.");
            }
            // Retrieve the patient from the database
            var patient = await _context.Patient.FirstOrDefaultAsync(p => p.ID == patientId);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            // Update the patient's email
            patient.Email = email;
            _context.SaveChanges();

            return Ok("Patient email updated successfully.");
        }
        //set phone
        [HttpPut("SetPatientPhone/{patientId}")]
        public IActionResult SetPatientPhone(int patientId, [FromForm] long Phone)
        {
            string PhonePattern = @"^07[7,8,9](\d){7}$";

            // Validate phone format
            if (!Regex.IsMatch(Phone.ToString(),PhonePattern))
            {
                return BadRequest("Invalid phone format.");
            }
            // Retrieve the patient from the database
            var patient = _context.Patient.FirstOrDefault(p => p.ID == patientId);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            // Update the patient's phone
            patient.PhoneNumber = Phone;
            _context.SaveChanges();

            return Ok("Patient phone updated successfully.");
        }
        [HttpGet("{patientId}/GetPatientInfo")]
        public ActionResult<Patient> GetPatientInfo(int patientId)
        {
            // Retrieve the patient from the database
            var patient = _context.Patient.FirstOrDefault(p => p.ID == patientId);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            return Ok(patient);
        }



        [HttpGet("{patientId}/GetInvoices")]
        public  IActionResult GetInvoices(int patientId)
        {
            // Retrieve the invoices from the database
            var invoices = _context.Invoice.Where(p => p.PatientID == patientId).ToList();
            if (invoices == null || invoices.Count == 0)
            {
                return NotFound("no invoices found.");
            }

            return Ok(invoices);
        }
        [HttpPost("{patientId}/PayInvoice")]
        public async Task<IActionResult> PayInvoice(int patientId, [FromForm] int IID)
        {

            // Retrieve the invoice from the database
            var invoice = await _context.Invoice.FirstOrDefaultAsync(i => i.ID == IID);
            if (invoice == null)
            {
                return NotFound("no invoice found.");
            }
            invoice.Status = true;
            _context.Invoice.Update(invoice);
            await _context.SaveChangesAsync();
            return Ok("Invoice Paid Successfully");
        }
        [HttpGet("{patientId}/ViewPersonalRecord")]
        public async Task<IActionResult> ViewPersonalRecord(int patientId)
        {

            // Retrieve the invoice from the database
            PatientRecord record = new PatientRecord();
            //retrieve tests
            var t = await _context.Test.Where(p => p.PatientID == patientId).ToListAsync();
            foreach (var rec in t)
                record.test.Append(rec);

            //retrieve radiology
            var r = await _context.Radiology.Where(p => p.PatientID == patientId).ToListAsync();
            foreach (var rec in r)
                record.radiology.Append(rec);

            //retrieve prescriptions
            var p = await _context.Prescription.Where(p => p.PatientID == patientId).ToListAsync();
            foreach (var rec in p)
                record.prescription.Append(rec);

            //retrieve appointments
            var a = await _context.Appointments.Where(p => p.PatientID == patientId).ToListAsync();
            foreach (var rec in a)
                record.appointments.Append(rec);
            
            return Ok(record);
        }

        [HttpPost("{patientid}/MakeAppointment")]
        public async Task<IActionResult> MakeAppointment(long patientid, [FromBody] AppointmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid appointment details");
            }

            // Ensure the patient exists
            var patient = await _context.Patient.FindAsync(request.PatientID);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            // Ensure the doctor exists
            var doctor = await _context.Doctor.FindAsync(request.DoctorID);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            // Create the appointment
            var appointment = new Appointment
            {
                DoctorID = request.DoctorID,
                AppointmentDate = request.AppointmentDate,
                Description = request.Description
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok("Appointment created successfully");
        }


    }
}
