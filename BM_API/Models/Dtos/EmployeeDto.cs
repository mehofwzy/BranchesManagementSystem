namespace BM_API.Models.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;

        public string? BranchName { get; set; }
        public Guid? BranchId { get; set; }
    }
}
