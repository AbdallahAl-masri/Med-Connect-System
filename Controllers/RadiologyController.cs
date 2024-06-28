using MCS.Entities;
using MCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCS.Controllers
{
    public class RadiologyController : Controller
    {
        private readonly McsContext _context;

        public RadiologyController(McsContext context)
        {
            _context = context;
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
            var results = await _context.Radiologies
                .Where(t => t.PatientName.Contains(searchTerm) || t.PatientId.ToString() == searchTerm)
                .Select(t => new TestResultModel
                {
                    PatientID = t.PatientId,
                    PatientName = t.PatientName,
                    TestType = t.ImageType,
                    TestDate = t.ImageDate,
                    Result = t.ImagePath,
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
                var testReport = new Radiology
                {
                    PatientId = model.PatientId,
                    PatientName = model.PatientName,
                    ImageType = model.TestName,
                    ImagePath = model.ReportContent,
                    ImageDate = model.ReportDate
                };

                _context.Radiologies.Add(testReport);
                _context.SaveChanges();

                return RedirectToAction("Index", "Radiology");
            }

            return View(model);
        }
    }
}
