using MCS.Data;
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
namespace MCS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("DoctorsInDepartment")]
        public async Task<IActionResult> DoctorsInDepartment([FromForm] int DeptID)
        {
            
            // Retrieve the department from the database
            var dept = await _context.Department.FirstOrDefaultAsync(d => d.ID == DeptID);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            var doc = await _context.DeptStaff.Where(d => d.DepartmentID == DeptID).Where(d=> d.Role == "Doctor").ToListAsync();
            return Ok(doc);
        }
        [HttpGet("StaffInDepartment")]
        public async Task<IActionResult> StaffInDepartment([FromForm] int DeptID)
        {

            // Retrieve the department from the database
            var dept = await _context.Department.FirstOrDefaultAsync(d => d.ID == DeptID);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            var staff = _context.DeptStaff.Where(d => d.DepartmentID == DeptID).Where(d => d.Role != "Doctor").ToListAsync();
            return Ok(staff);
        }
        [HttpGet("ViewDepartments")]
        public async Task<IActionResult> ViewDepartments()
        {
            var dept = await _context.Department.ToListAsync();
            return Ok(dept);
        }
        [HttpGet("ViewDepartment")]
        public async Task<IActionResult> ViewDepartment([FromForm] int DeptID)
        {

            // Retrieve the department from the database
            var dept = await _context.Department.FirstOrDefaultAsync(d => d.ID == DeptID);
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
            var clinic = await _context.Clinic.ToListAsync();
            return Ok(clinic);
        }

        [HttpPost("AddClinic")]
        public async Task<IActionResult> AddClinic([FromForm] Clinic clinicx)
        {

            var clinic =  _context.Clinic.Where(c => c.ID == clinicx.ID);
            if (clinic != null)
            {
                return BadRequest("clinic already found.");
            }
            await _context.Clinic.AddAsync(clinicx);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("RemoveClinic")]
        public async Task<IActionResult> RemoveClinic([FromForm] int id)
        {

            var clinic = await _context.Clinic.FirstOrDefaultAsync(c => c.ID == id);
            if (clinic == null)
            {
                return NotFound("Clinic not found.");
            }
            _context.Clinic.Remove(clinic);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromForm] Department dept)
        {

            var dep = _context.Department.Where(c => c.ID == dept.ID);
            if (dep != null)
            {
                return BadRequest("Department already found.");
            }
            await _context.Department.AddAsync(dept);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("RemoveDepartment")]
        public async Task<IActionResult> RemoveDepartment([FromForm] int id)
        {

            var dept = await _context.Department.FirstOrDefaultAsync(c => c.ID == id);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            _context.Department.Remove(dept);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromForm] Doctor doc)
        {

            var docx = _context.Doctor.Where(c => c.ID == doc.ID);
            if (docx != null)
            {
                return BadRequest("Doctor already found.");
            }
            await _context.Doctor.AddAsync(doc);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor([FromForm] int id)
        {

            var doc = await _context.Doctor.FirstOrDefaultAsync(c => c.ID == id);
            if (doc == null)
            {
                return NotFound("Department not found.");
            }
            _context.Doctor.Remove(doc);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("AddStaff")]
        public async Task<IActionResult> AddStaff([FromForm] DeptStaff staff)
        {

            var dep = await _context.Department.FirstOrDefaultAsync(c => c.ID == staff.DepartmentID);

            if (dep == null)
            {
                return BadRequest("Department not found.");
            }

            if (staff == null)
            {
                return BadRequest("Missing Data");
            }

            await _context.DeptStaff.AddAsync(staff);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("RemoveStaff")]
        public async Task<IActionResult> RemoveStaff([FromForm] int id, [FromBody] int did)
        {

            var dept = await _context.Department.FirstOrDefaultAsync(c => c.ID == did);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            var staff = await _context.DeptStaff.FirstOrDefaultAsync(s => s.StaffID == id);
            if (staff == null)
            {
                return NotFound("No Staff in Department with such ID");
            }
            _context.DeptStaff.Remove(staff);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
