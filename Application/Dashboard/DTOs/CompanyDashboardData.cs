using Application.SoftwareProjects.DTOs.Dashboard;
using Domain.ModelsDTOs;

namespace Application.Dashboard.DTOs
{
    public class CompanyDashboardData
    {
        public List<SoftwareCompanyDto> AllClients { get; set; }
        public List<SoftwareProjectDashboardDto> AllProjects { get; set; }
        public List<ProjectManagerDto> AllProjectManagers { get; set; }
        public List<DeveloperDto> AllDevelopers { get; set; }
    }
}
