namespace Domain
{
    public class ProjectPhase
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SoftwareProject Project { get; set; }
        public ICollection<Requirement> Requirements { get; set; }
    }
}