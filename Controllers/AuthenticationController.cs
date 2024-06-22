using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MCS.Models;
using Microsoft.EntityFrameworkCore;
using MCS.Entities;

namespace MCS.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<DeptStaff> _userManager;
        private readonly SignInManager<DeptStaff> _signInManager;
        private readonly McsContext _context;

        public AuthenticationController(UserManager<DeptStaff> userManager, SignInManager<DeptStaff> signInManager, McsContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpPost("register")]
        public async Task<IActionResult> PatientRegister([FromBody] RegisterModel model)
        {
            var username = model.Email ?? model.Phone;

            if (ModelState.IsValid && model.Password == model.ConfirmPassword)
            {
                var user = new DeptStaff { UserName = username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "User registered successfully" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

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
            return View(new StaffLoginModel());
        }

        [HttpPost("StaffLogin")]
        public async Task<IActionResult> StaffLogin(StaffLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var staff = await _context.DeptStaffs
                    .FirstOrDefaultAsync(s => s.StaffId == model.StaffID && s.DepartmentId == model.DepartmentID);

                if (staff == null)
                {
                    return BadRequest("Invalid staff ID or department.");
                }

                // Verify password 
                // Here we assume `PasswordHash` is the hashed password stored in the database
                var passwordHasher = new PasswordHasher<DeptStaff>(); 
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(staff, staff.PasswordHash, model.Password);

                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                    return BadRequest("Invalid password.");
                }

                // If you use token-based authentication, you could generate a JWT token here
                // For this example, we'll return a simple success message
                return Ok(new { Message = "Login successful", Staff = staff });
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("StaffLogin", "Authentication");
        }
    }

}
