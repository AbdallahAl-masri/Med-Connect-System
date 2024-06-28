using MCS.Entities;
using MCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCS.Controllers
{
    public class LabTechnicianController : Controller
    {
        private readonly McsContext _context;
        public LabTechnicianController(McsContext mcsContext) 
        {
            _context = mcsContext;
        }

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
        public async Task<IActionResult> SearchPatient(string searchTerm)
        {
            var results = await _context.Tests
                .Where(t => t.PatientName.Contains(searchTerm) || t.PatientId.ToString() == searchTerm)
                .Select(t => new TestResultModel
                {
                    PatientID = t.PatientId,
                    PatientName = t.PatientName,
                    TestType = t.TestType,
                    TestDate = t.TestDate,
                    Result = t.Results,
                    Technician = t.Technician
                }).ToListAsync();

            return View(results);
        }

        [HttpGet]
        public IActionResult UploadTestResult()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadTestResult(TestReportModel model)
        {
            if (ModelState.IsValid)
            {
                var testReport = new Test
                {
                    PatientId = model.PatientId,
                    PatientName = model.PatientName,
                    TestType = model.TestName,
                    Results = model.ReportContent,
                    TestDate = model.ReportDate,
                    RecievedResult = true,
                };

                _context.Tests.Add(testReport);
                _context.SaveChanges();

                return RedirectToAction("Index", "LabTechnician");
            }

            return View(model);
        }
    }
}
