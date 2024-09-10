using Domain.ModelsDTOs;

namespace Application.SoftwareProjects.DTOs
{
    public class UpdateRequirementLayoutDto
    {
        public Guid ProjectId { get; set; }
        public List<ProjectPhaseDto> ProjectPhases { get; set; }
    }
}