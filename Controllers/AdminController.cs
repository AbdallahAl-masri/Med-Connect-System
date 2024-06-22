using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MCS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MCS.Entities;
namespace MCS.Controllers
{
    public class AdminController : Controller
    {
        private readonly McsContext _context;
        public AdminController(McsContext context)
        {
            _context = context;
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
            var staff = await _context.DeptStaffs.FirstOrDefaultAsync(s => s.StaffId == id);
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
                .Where(e => e.DepartmentId == id /*|| e.StaffId == id*/)
                .ToListAsync();

            var viewModel = new ManageEmployeesViewModel
            {
                SearchId = searchid,
                Employees = employees
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.DeptStaffs.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DeptStaff employee)
        {
            if (ModelState.IsValid)
            {
                _context.Update(employee);
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
        public async Task<IActionResult> CreateCredentials(DeptStaff model)
        {
            if (ModelState.IsValid)
            {
                // You may want to add more fields or validations here
                _context.DeptStaffs.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View("AssignLoginInfo", model);
        }
    }
}
