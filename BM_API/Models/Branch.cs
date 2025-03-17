using System.ComponentModel.DataAnnotations;

namespace BM_API.Models
{
    public class Branch
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
        public ICollection<MaintenanceRequest>? MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
    }
}
