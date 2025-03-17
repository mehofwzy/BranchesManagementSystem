using System.Linq.Expressions;
using BM_API.Data;
using BM_API.Models;
using BM_API.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")] // Only Admin can manage employees
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Branch)
                .Select(e => new EmployeeDto
                {
                    Id=e.Id,
                    Name = e.Name,
                    Role = e.Role,
                    BranchName= e.Branch != null ? e.Branch.Name : null,
                    BranchId = e.BranchId
                    
                })
                .OrderBy(o => o.Name).ToListAsync();

            return Ok(employees);
        }

        // Get Employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _context.Employees
                .Include(e => e.Branch)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Role = e.Role,
                    BranchName = e.Branch != null ? e.Branch.Name : null,
                    BranchId = e.BranchId
                })
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            return Ok(employee);
        }

        // Add a New Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employee
            {
                Id = Guid.NewGuid(), // a new GUID is generated
                Name = employeeDto.Name,
                Role = employeeDto.Role,
                BranchId = employeeDto.BranchId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employeeDto);
        }

        // Update Employee
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            employee.Name = employeeDto.Name;
            employee.Role = employeeDto.Role;
            employee.BranchId = employeeDto.BranchId;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Employee updated successfully" });
        }

        // Delete Employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Employee deleted successfully" });
        }
    }
}
