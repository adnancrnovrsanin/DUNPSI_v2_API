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
        public RequirementApproveStatus Status { get; set; }
        public SoftwareProject Project { get; set; }
        public ProjectPhase Phase { get; set; }
        public ICollection<RequirementManagement> Assignees { get; set; }
        public ICollection<Rating> DeveloperRatings { get; set; }
    }
}