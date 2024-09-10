using Domain.ModelsDTOs;

namespace Application.SoftwareProjects
{
    public class ProjectCreateDto
    {
        public Guid ProjectRequestId { get; set; }
        public Guid AssignedProjectManager { get; set; }
        public List<Guid> SelectedDevelopers { get; set; }
    }
}