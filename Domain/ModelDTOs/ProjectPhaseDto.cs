namespace Domain.ModelsDTOs
{
    public class ProjectPhaseDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RequirementDto> Requirements { get; set; }
    }
}