
namespace BM_Web.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string? BranchName { get; set; }
        public Guid? BranchId { get; set; }
    }
}
