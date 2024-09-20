namespace Domain
{
    public class Requirement
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid PhaseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SerialNumber { get; set; }
        public int IdNumber { get; set; }
        public RequirementApproveStatus Status { get; set; }
        public SoftwareProject Project { get; set; }
        public ProjectPhase Phase { get; set; }
        public RequirementType Type { get; set; }
        public RequirementPriority Priority { get; set; }
        public int Estimate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<RequirementManagement> Assignees { get; set; }
        public Rating QualityRating { get; set; }
    }
}