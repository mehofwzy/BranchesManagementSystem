using Azure.Core;
using BM_API.Data;
using BM_API.Models;
using BM_API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRequestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Maintenance Requests
        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _context.MaintenanceRequests
                .Include(m => m.Branch)
                .Select(m => new MaintenanceRequestDto
                {
                    Id = m.Id,
                    Description = m.Description,
                    Status =m.Status,
                    BranchId = m.BranchId,
                    BranchName = m.Branch!.Name
                })
                .ToListAsync();

            return Ok(requests);
        }

        // Get Maintenance Request by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(Guid id)
        {
            var request = await _context.MaintenanceRequests
                .Include(m => m.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (request == null)
                return NotFound(new { Message = "Maintenance request not found" });

            return Ok(new MaintenanceRequestDto
            {
                Id = request.Id,
                Description = request.Description,
                Status = request.Status,
                BranchId = request.BranchId,
                BranchName = request.Branch!.Name
            });
        }

        // Create Maintenance Request
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] MaintenanceRequestDto requestDto)
        {
            var branch = await _context.Branches.FindAsync(requestDto.BranchId);
            if (branch == null)
                return BadRequest(new { Message = "Invalid Branch ID" });

            var request = new MaintenanceRequest
            {
                Id = Guid.NewGuid(),
                Description = requestDto.Description,
                Status = requestDto.Status ?? "Pending",
                BranchId = requestDto.BranchId
            };

            _context.MaintenanceRequests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRequestById), new { id = request.Id }, requestDto);
        }

        // Update Maintenance Request Status
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(Guid id, [FromBody] MaintenanceRequestDto requestDto)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id);
            if (request == null)
                return NotFound(new { Message = "Maintenance request not found" });

            request.Description = requestDto.Description;
            request.Status = requestDto.Status ?? request.Status;
            request.BranchId = requestDto.BranchId;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Maintenance request updated successfully" });
        }

        // Delete Maintenance Request
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id);
            if (request == null)
                return NotFound(new { Message = "Maintenance request not found" });

            _context.MaintenanceRequests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Maintenance request deleted successfully" });
        }
    }
}