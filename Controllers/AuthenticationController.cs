using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MCS.Models;
using Microsoft.EntityFrameworkCore;
using MCS.Entities;
using System.Security.Principal;

namespace MCS.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<DeptStaff> _userManager;
        private readonly SignInManager<DeptStaff> _signInManager;
        private readonly UserManager<Patient> _patientuserManager;
        private readonly McsContext _context;

        public AuthenticationController(UserManager<DeptStaff> userManager, SignInManager<DeptStaff> signInManager, McsContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        [HttpPost]
        [Route("PatientRegister")]
        public async Task<IActionResult> PatientRegister(string firstname , string lastname, DateTime dateofbirth,string password , long phonenumber , string officialid)
        {
            

            
            var user = new Patient 
            {
                Name = firstname + " " + lastname,
                BirthDate = dateofbirth.Date,
                PhoneNumber = phonenumber,
                OfficialId = officialid,
                PasswordHash = 
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                    return Ok(new { Message = "User registered successfully" },PatientId = );
            }

            foreach (var error in result.Errors)
            {
                    ModelState.AddModelError(string.Empty, error.Description);
            }

           

            return BadRequest("Invalid details");
        }
        [HttpGet("PatientLogin")]
        public async Task<IActionResult> PatientLogin([FromForm] PatientLoginModel model)
        {
            var username = model.Email ?? model.Phone;
            if (ModelState.IsValid)
            {
                
                var result = await _signInManager.PasswordSignInAsync(username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "User logged in successfully" });
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

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
