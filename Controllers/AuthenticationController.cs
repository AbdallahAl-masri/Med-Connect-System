using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MCS.Models;
using Microsoft.EntityFrameworkCore;
using MCS.Entities;
using System.Security.Principal;
using System.Globalization;

namespace MCS.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<DeptStaff> _userManager;
        private readonly SignInManager<DeptStaff> _signInManager;
        private readonly UserManager<Patient> _patientuserManager;
        private readonly SignInManager<Patient> _patientsignInManager;

        private readonly McsContext _context;

        public AuthenticationController(UserManager<DeptStaff> userManager, SignInManager<DeptStaff> signInManager, McsContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        [HttpPost]
        [Route("PatientRegister")]
        public async Task<IActionResult> PatientRegister(string firstname , string lastname, string dateofbirth, long phonenumber , string nationalnumber)
        {
            var user = new Patient 
            {
                Name = firstname + " " + lastname,
                BirthDate = DateTime.ParseExact(dateofbirth, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                PhoneNumber = phonenumber,
                OfficialId = nationalnumber,
            };
            _context.Add(user);
            await _context.SaveChangesAsync();
            user = await _context.Patients.FindAsync(user);
            return Ok(new { patientid = user.Id } );
        }
        [HttpPost]
        [Route("PatientRegister2")]
        public async Task<IActionResult> PatientRegister2(long Id , string Password)
        {

            var user = await _context.Patients.FirstOrDefaultAsync(p => p.Id == Id);
            if (user == null)
                return BadRequest(new { Message = "User not found" } );
            var result = await _patientuserManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" } );
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest("Invalid details");
        }
        [HttpGet("PatientLogin")]
        [Route("PatientLogin")]

        public async Task<IActionResult> PatientLogin(string nationalnumber , string password)
        {
          
           
            var result = await _patientsignInManager.PasswordSignInAsync(nationalnumber, password, false, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(p => p.OfficialId == nationalnumber);
                return Ok(new { patientid = patient.Id });
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return BadRequest(ModelState);
           
        }

        [HttpGet]
        public IActionResult StaffLogin()
        {
            //_signInManager.SignOutAsync();
            ModelState.Clear();
            return View(new StaffLoginModel());
        }

        [HttpPost("StaffLogin")]
        public async Task<IActionResult> StaffLogin(StaffLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var staff = await _userManager.FindByIdAsync(model.StaffID);

                if (staff == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Staff ID.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(staff.UserName, model.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    TempData["UserName"] = staff.UserName + " " + staff.LastName;
                    // Check role and redirect accordingly
                    switch (staff.Role)
                    {
                        case "Doctor":
                            return RedirectToAction("Index", "Doctor");
                        case "Pharmacist":
                            return RedirectToAction("Index", "Pharmacist");
                        case "Lab Technician":
                            return RedirectToAction("Index", "LabTechnician");
                        case "Radiology Technician":
                            return RedirectToAction("Index", "RadiologyTechnician");
                        case "Staff":
                            return RedirectToAction("Index", "Medical_Personnel");
                        default:
                            return RedirectToAction("Index", "Admin");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult Logout()
        {
            return RedirectToAction("StaffLogin", "Authentication");
        }

    }

}
