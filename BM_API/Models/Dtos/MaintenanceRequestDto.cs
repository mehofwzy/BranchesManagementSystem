namespace BM_API.Models.Dtos
{
    public class MaintenanceRequestDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Status { get; set; } = "Pending"; // Optional override
        public string? BranchName { get; set; }
        public Guid BranchId { get; set; }
    }
}