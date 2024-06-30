using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using MCS.Models;
using MCS.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlTypes;

namespace MCS.Controllers
{
    public class AdminController : Controller
    {
        private readonly McsContext _context;
        private readonly UserManager<DeptStaff> _userManager;
        public AdminController(McsContext context, UserManager<DeptStaff> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("DoctorsInDepartment")]
        public async Task<IActionResult> DoctorsInDepartment([FromForm] int DeptID)
        {

            // Retrieve the department from the database
            var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == DeptID);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            var doc = await _context.DeptStaffs.Where(d => d.DepartmentId == DeptID).Where(d => d.Role == "Doctor").ToListAsync();
            return Ok(doc);
        }
        [HttpGet("StaffInDepartment")]
        public async Task<IActionResult> StaffInDepartment([FromForm] int DeptID)
        {

            // Retrieve the department from the database
            var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == DeptID);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            var staff = _context.DeptStaffs.Where(d => d.DepartmentId == DeptID);
            return Ok(staff);
        }
        [HttpGet("ViewDepartments")]
        public async Task<IActionResult> ViewDepartments()
        {
            var dept = await _context.Departments.ToListAsync();
            return Ok(dept);
        }
        [HttpGet("ViewDepartment")]
        public async Task<IActionResult> ViewDepartment([FromForm] int DeptID)
        {

            // Retrieve the department from the database
            var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == DeptID);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            return Ok(dept);
        }
        [HttpGet("ViewClinics")]
        public async Task<IActionResult> ViewClinics()
        {

            // Retrieve the department from the database
            var clinic = await _context.Clinics.ToListAsync();
            return Ok(clinic);
        }
        [HttpPost("AddClinic")]
        public async Task<IActionResult> AddClinic([FromForm] Clinic clinicx)
        {

            var clinic = _context.Clinics.Where(c => c.Id == clinicx.Id);
            if (clinic != null)
            {
                return BadRequest("clinic already found.");
            }
            await _context.Clinics.AddAsync(clinicx);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("RemoveClinic")]
        public async Task<IActionResult> RemoveClinic([FromForm] int id)
        {

            var clinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Id == id);
            if (clinic == null)
            {
                return NotFound("Clinic not found.");
            }
            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromForm] Department dept)
        {

            var dep = _context.Departments.Where(c => c.Id == dept.Id);
            if (dep != null)
            {
                return BadRequest("Department already found.");
            }
            await _context.Departments.AddAsync(dept);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("RemoveDepartment")]
        public async Task<IActionResult> RemoveDepartment([FromForm] int id)
        {

            var dept = await _context.Departments.FirstOrDefaultAsync(c => c.Id == id);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromForm] Doctor doc)
        {

            var docx = _context.Doctors.Where(c => c.Id == doc.Id);
            if (docx != null)
            {
                return BadRequest("Department already found.");
            }
            await _context.Doctors.AddAsync(doc);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor([FromForm] int id)
        {

            var doc = await _context.Doctors.FirstOrDefaultAsync(c => c.Id == id);
            if (doc == null)
            {
                return NotFound("Department not found.");
            }
            _context.Doctors.Remove(doc);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("AddStaff")]
        public async Task<IActionResult> AddStaff([FromForm] DeptStaff staff)
        {

            var dep = await _context.Departments.FirstOrDefaultAsync(c => c.Id == staff.DepartmentId);
            if (dep == null)
            {
                return BadRequest("Department not found.");
            }
            if (staff == null)
            {
                return BadRequest("Missing Data");

            }
            await _context.DeptStaffs.AddAsync(staff);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("RemoveStaff")]
        public async Task<IActionResult> RemoveStaff([FromForm] int id, [FromBody] int did)
        {

            var dept = await _context.Departments.FirstOrDefaultAsync(c => c.Id == did);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            var staff = await _context.DeptStaffs.FirstOrDefaultAsync(s => s.Id == id);
            if (staff == null)
            {
                return NotFound("No Staff in Department with such ID");
            }
            _context.DeptStaffs.Remove(staff);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userName = TempData["UserName"] as string;
            ViewBag.UserName = userName;
            return View();
        }

        [HttpGet]
        public IActionResult SearchEmployees()
        {
            var viewModel = new ManageEmployeesViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SearchEmployees(string searchid)
        {
            int.TryParse(searchid, out int id);

            var employees = await _context.DeptStaffs
                .Where(e => e.Id == id)
                .ToListAsync();

            var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == employees.First().DepartmentId);

            var viewModel = new ManageEmployeesViewModel
            {
                SearchId = searchid,
                department = dept.Name,
                Employees = employees
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var employee = await _context.DeptStaffs.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            var model = new DeptStaffModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DepartmentId = employee.DepartmentId,
                Role = employee.Role,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                StaffId = employee.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DeptStaffModel employee)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = await _context.DeptStaffs.FindAsync(employee.StaffId);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Update fields except password
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.Role = employee.Role;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;

                // Only update password if it's not empty
                if (!string.IsNullOrEmpty(employee.Password))
                {
                    var passwordHasher = new PasswordHasher<DeptStaffModel>();
                    var hashedPassword = passwordHasher.HashPassword(employee, employee.Password);
                    existingEmployee.PasswordHash = hashedPassword;
                }

                _context.Update(existingEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SearchEmployees));
            }
            return View(employee);
        }


        // Delete Actions
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.DeptStaffs.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.DeptStaffs.FindAsync(id);
            _context.DeptStaffs.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SearchEmployees));
        }

        [HttpGet]
        public IActionResult AssignLoginInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCredentials(DeptStaffModel model)
        {
            
                var validUserName = GenerateValidUserName(model.FirstName);
                var existingUser = await _userManager.FindByNameAsync(validUserName);

                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Username already exists. Please choose a different username.");
                    return View("AssignLoginInfo", model);
                }

                if (model.Password == null)
                {
                    ModelState.AddModelError(string.Empty, "Please insert password");
                    return View("AssignLoginInfo", model);
                }

                var staff = new DeptStaff
                {
                    UserName = validUserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DepartmentId = model.DepartmentId,
                    Role = model.Role,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(staff, model.Password);

                if (result.Succeeded)
                {
                    if (model.Role == "Doctor")
                    {
                        if (string.IsNullOrWhiteSpace(model.Speciality))
                        {
                            ModelState.AddModelError(string.Empty, "Please enter a specialty for the doctor.");
                            return View("AssignLoginInfo", model);
                        }
                        var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == model.DepartmentId);
                        var doc = new Doctor
                        {
                            Name = model.FirstName + " " + model.LastName,
                            Speciality = model.Speciality,
                            StaffId = model.StaffId,
                            PhoneNumber = long.Parse(model.PhoneNumber),
                            ClinicId = model.DepartmentId,
                        };
                        _context.Doctors.Add(doc);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            

            return View("AssignLoginInfo",model);
        }

        private string GenerateValidUserName(string firstName)
        {
            // Remove invalid characters and ensure the username is not empty
            var validUserName = Regex.Replace(firstName, @"[^a-zA-Z0-9\-_\.@]+", "");
            if (string.IsNullOrWhiteSpace(validUserName))
            {
                validUserName = Guid.NewGuid().ToString();
            }
            return validUserName;
        }


        [HttpGet]
        public IActionResult SearchDepartments()
        {
            var viewModel = new ManageEmployeesViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SearchDepartments(string searchid)
        {
            if (long.TryParse(searchid, out long id))
            {
                try
                {
                    var employees = await _context.DeptStaffs
                        .Where(e => e.DepartmentId == id)
                        .ToListAsync();
                    var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == employees.First().DepartmentId);
                    if (employees == null || !employees.Any())
                    {
                        ModelState.AddModelError(string.Empty, "No employees found for the provided Department ID.");
                    }

                    var viewModel = new ManageEmployeesViewModel
                    {
                        SearchId = searchid,
                        Employees = employees,
                        department = dept.Name,
                    };

                    return View(viewModel);
                }
                catch (SqlNullValueException ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while retrieving employees: " + ex.Message);
                    return View(new ManageEmployeesViewModel { SearchId = searchid });
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Department ID.");
            return View(new ManageEmployeesViewModel { SearchId = searchid });
        }

    }
}
