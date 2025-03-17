using BM_API.Data;
using BM_API.Models;
using BM_API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Branches
        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var branches = await _context.Branches
                .Select(b => new BranchDto
                {
                    Id=b.Id,
                    Name = b.Name,
                    Location = b.Location
                })
                .ToListAsync();

            return Ok(branches);
        }

        // Get Branch by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(Guid id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
                return NotFound(new { Message = "Branch not found" });

            return Ok(new BranchDto
            {
                Id= branch.Id,
                Name = branch.Name,
                Location = branch.Location
            });
        }

        // Create Branch
        [HttpPost]
        public async Task<IActionResult> CreateBranch([FromBody] BranchDto branchDto)
        {
            var branch = new Branch
            {
                Id = Guid.NewGuid(),
                Name = branchDto.Name,
                Location = branchDto.Location
            };

            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBranchById), new { id = branch.Id }, branchDto);
        }

        // Update Branch
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(Guid id, [FromBody] BranchDto branchDto)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
                return NotFound(new { Message = "Branch not found" });

            branch.Name = branchDto.Name;
            branch.Location = branchDto.Location;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Branch updated successfully" });
        }

        // Delete Branch
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(Guid id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
                return NotFound(new { Message = "Branch not found" });

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Branch deleted successfully" });
        }
    }
}
