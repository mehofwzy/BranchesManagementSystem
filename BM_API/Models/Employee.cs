using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BM_API.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Role { get; set; }

        public Guid? BranchId { get; set; }
        [JsonIgnore]
        public Branch? Branch { get; set; }
    }
}

