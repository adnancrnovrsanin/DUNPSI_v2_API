namespace Domain
{
    public class Team
    {
        public Guid Id { get; set; }
        public Guid ProjectManagerId { get; set; }
        public SoftwareProject Project { get; set; }
        public ProjectManager Manager { get; set; }
        public ICollection<DeveloperTeamPlacement> AssignedDevelopers { get; set; }
    }
}