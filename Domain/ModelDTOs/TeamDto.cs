namespace Domain.ModelsDTOs
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public Guid ProjectManagerId { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectManagerDto Manager { get; set; }
        public ICollection<DeveloperDto> Developers { get; set; }
    }
}