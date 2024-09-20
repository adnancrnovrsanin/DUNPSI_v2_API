using Domain;
using Domain.ModelsDTOs;

namespace Application.SoftwareProjects.DTOs.Dashboard
{
    public class ProjectPhaseDashboardDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RequirementDto> Requirements { get; set; }
    }
}
