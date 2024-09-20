using Domain;
using Domain.ModelsDTOs;

namespace Application.SoftwareProjects.DTOs.Dashboard
{
    public class SoftwareProjectDashboardDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public Guid AssignedTeamId { get; set; }
        public TeamDto AssignedTeam { get; set; }
        public SoftwareCompanyDto Client { get; set; }
        public ICollection<ProjectPhaseDashboardDto> Phases { get; set; }
    }
}
