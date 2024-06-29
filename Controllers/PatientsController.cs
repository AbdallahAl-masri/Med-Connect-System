using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MCS.Entities;
using MCS.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
namespace MCS.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class PatientsController : Controller
    {
        private readonly McsContext _context;
      
        public PatientsController(McsContext context)
        {
            _context = context;
        }
        
        

        [HttpGet]
        [Route("GetPatientAppointments")]
        public ActionResult<IEnumerable<PatientAppointment>> GetPatientAppointments(int patientId)
        {
            // Retrieve appointments for the specified patient and structure them into a list
            var appointments = _context.PatientAppointments
                .Where(a => a.PatientId == patientId)
                .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                return NotFound("No appointments found for the specified patient.");
            }
            // will return the appointments as a list where we will parse them as json in the mobile app and display them
            return Ok(appointments);
        }

        // GET: api/Patient/PatientRadiology/{patientId}
        [HttpGet]
        [Route("GetPatientRadiology")]
        public ActionResult<IEnumerable<PatientRadiology>> GetPatientRadiology(int patientId)
        {
            // Retrieve images for the specified patient and structure them into a list
            var radiology = _context.PatientRadiologies
                .Where(r => r.PatientId == patientId)
                .ToList();

            if (radiology == null || radiology.Count == 0)
            {
                return NotFound("No images found for the specified patient.");
            }
            // will return the images as a list where we will parse them as json in the mobile app and display them
            return Ok(radiology);
        }
        [HttpGet]
        [Route("GetpatientTests")]
        public ActionResult<IEnumerable<Test>> GetPatientTests(int patientId)
        {
            // Retrieve images for the specified patient and structure them into a list
            var tests = _context.Tests
                .Where(t => t.PatientId == patientId)
                .ToList();

            if (tests == null || tests.Count == 0)
            {
                return NotFound("No images found for the specified patient.");
            }
            // will return the images as a list where we will parse them as json in the mobile app and display them
            return Ok(tests);
        }

        //set email
        [HttpPut]
        [Route("SetPatientEmail")]

        public async Task<IActionResult> SetPatientEmail(int patientId, [FromForm] string email)
        {
            string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            // Validate email format
            if (!Regex.IsMatch(email, emailPattern))
            {
                return BadRequest("Invalid email format.");
            }
            // Retrieve the patient from the database
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
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
        [HttpPut]
        [Route("SetPatientPhone")]

        public IActionResult SetPatientPhone(int patientId, [FromForm] long Phone)
        {
            string PhonePattern = @"^07[7,8,9](\d){7}$";

            // Validate phone format
            if (!Regex.IsMatch(Phone.ToString(),PhonePattern))
            {
                return BadRequest("Invalid phone format.");
            }
            // Retrieve the patient from the database
            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            // Update the patient's phone
            patient.PhoneNumber = Phone;
            _context.SaveChanges();

            return Ok("Patient phone updated successfully.");
        }
        [HttpGet]
        [Route("GetPatientInfo")]

        public ActionResult<Patient> GetPatientInfo(int patientId)
        {
            // Retrieve the patient from the database
            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            return Ok(patient);
        }



        [HttpGet]
        [Route("GetInvoices")]

        public IActionResult GetInvoices(int patientId)
        {
            // Retrieve the invoices from the database
            var invoices = _context.Invoices.Where(p => p.PatientId == patientId).ToList();
            if (invoices == null || invoices.Count == 0)
            {
                return NotFound("no invoices found.");
            }

            return Ok(invoices);
        }
        [HttpPost]
        [Route("PayInvoice")]

        public async Task<IActionResult> PayInvoice(int patientId, [FromForm] int IID)
        {

            // Retrieve the invoice from the database
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == IID);
            if (invoice == null)
            {
                return NotFound("no invoice found.");
            }
            invoice.Status = true;
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
            return Ok("Invoice Paid Successfully");
        }
        [HttpGet]
        [Route("ViewPersonalRecord")]

        public async Task<IActionResult> ViewPersonalRecord(int patientId)
        {

            var record = new List<PatientRecordView>();
            var r = await _context.Diagnoses.Where(d => d.PatientId == patientId).ToListAsync();
            foreach(var d in r)
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(doc => doc.Id == d.DoctorId);
                var t = new PatientRecordView
                {
                    DiagnosisDate = d.DiagnosisDate.Date,
                    diagnosis = d.Diagnosis1,
                    DoctorName = doctor.Name
                };
                record.Append(t);
            }
            return Ok(record);
        }

        [HttpPost]
        [Route("MakeAppointment")]
        public async Task<IActionResult> MakeAppointment(long patientid, string departmentname, DateTime apptdate, string period,string doctorname="")
        {
           

            
            var doc = await _context.Doctors.FindAsync(doctorname);
           
            Appointment appointment;
            // Create the appointment
            if (doctorname=="")
            {
                var dept = await _context.Departments.FindAsync(departmentname);
                appointment = new Appointment
                {
                    DepartmentId = dept.Id,
                    PatientId = patientid,
                    Timeslot = apptdate.Date,
                    Period = period
                };
            }
            else
            {
                var dept = await _context.Departments.FindAsync(departmentname);
                appointment = new Appointment
                {
                    DepartmentId = dept.Id,
                    PatientId = patientid,
                    Timeslot = apptdate.Date,
                    DoctorId = doc.Id,
                    Period = period
                };
            }
            
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok("Appointment created successfully");
        }
        
        [HttpGet]
        [Route("GetPrescriptions")]

        public async Task<IActionResult> GetPrescriptions(int patientId)
        {


            var prescriptions = await _context.Prescriptions.Where(p => p.PatientId == patientId).ToListAsync();
            var prescs = new List<PatientPrescription>();

            foreach (var prescription in prescriptions) {
                var rec = new PatientPrescription
                {
                    Date = prescription.CreatedDate.Date,
                    Meds = prescription.Medications.ToList()
                };
                prescs.Append(rec);
            }

            return Ok(prescs);
        }


    }
}
