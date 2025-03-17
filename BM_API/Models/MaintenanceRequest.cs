using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class MaintenanceRequest
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Status { get; set; } = "Pending";
        public Branch? Branch { get; set; }
    }
}
