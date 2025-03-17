namespace BM_API.Models.Dtos
{
    public class BranchDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
