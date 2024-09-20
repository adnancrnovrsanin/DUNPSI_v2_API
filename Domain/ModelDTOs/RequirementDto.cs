namespace Domain.ModelsDTOs
{
    public class RequirementDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid PhaseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int SerialNumber { get; set; }
        public int IdNumber { get; set; }
        public string Type { get; set; }
        public int Priority { get; set; }
        public int Estimate { get; set; }
        public string CreatedAt { get; set; }
        public ICollection<DeveloperDto> AssignedDevelopers { get; set; }
    }
}