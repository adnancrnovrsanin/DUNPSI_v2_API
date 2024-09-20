using System.Globalization;
using Application.SoftwareCompanies.DTOs;
using Application.SoftwareProjects.DTOs.Dashboard;
using AutoMapper;
using Domain;
using Domain.ModelDTOs;
using Domain.ModelsDTOs;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap<Source, Destination>();
            CreateMap<AppUser, AppUser>();
            CreateMap<SoftwareProject, SoftwareProject>();
            CreateMap<SoftwareCompany, SoftwareCompany>();
            CreateMap<Team, Team>();
            CreateMap<ProjectManager, ProjectManager>();
            CreateMap<ProductManager, ProductManager>();
            CreateMap<Developer, Developer>();
            CreateMap<Message, Message>();
            CreateMap<Rating, Rating>();
            CreateMap<Requirement, Requirement>();

            CreateMap<ProjectPhase, ProjectPhaseDashboardDto>();
            CreateMap<SoftwareProject, SoftwareProjectDashboardDto>();

            CreateMap<Developer, DeveloperDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.AppUser.Name))
                .ForMember(d => d.Surname, o => o.MapFrom(s => s.AppUser.Surname))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.AppUser.Email))
                .ForMember(d => d.ProfileImageUrl, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(d => d.AppUserId, o => o.MapFrom(s => s.AppUser.Id));
            CreateMap<ProjectManager, ProjectManagerDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.AppUser.Name))
                .ForMember(d => d.Surname, o => o.MapFrom(s => s.AppUser.Surname))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.AppUser.Email))
                .ForMember(d => d.ProfileImageUrl, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(d => d.CurrentTeamId, o => o.MapFrom(s => s.ManagedTeams.FirstOrDefault(t => t.Project.Status != ProjectStatus.COMPLETED).Id));
            CreateMap<ProductManager, ProductManagerDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.AppUser.Name))
                .ForMember(d => d.Surname, o => o.MapFrom(s => s.AppUser.Surname))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.AppUser.Email))
                .ForMember(d => d.ProfileImageUrl, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url));
            CreateMap<Admin, AdminDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.AppUser.Name))
                .ForMember(d => d.Surname, o => o.MapFrom(s => s.AppUser.Surname))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.AppUser.Email))
                .ForMember(d => d.ProfileImageUrl, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url));
            CreateMap<Team, TeamDto>()
                .ForMember(t => t.ProjectId, o => o.MapFrom(s => s.Project.Id))
                .ForMember(t => t.Developers, o => o.MapFrom(s => s.AssignedDevelopers.Select(d => d.Developer)));
            CreateMap<SoftwareProject, SoftwareProjectDto>()
                .ForMember(p => p.DueDate, o => o.MapFrom(s => s.DueDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)));
            CreateMap<SoftwareCompany, SoftwareCompanyDto>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.AppUser.Email))
                .ForMember(d => d.RepresentativeName, o => o.MapFrom(s => s.AppUser.Name))
                .ForMember(d => d.RepresentativeSurname, o => o.MapFrom(s => s.AppUser.Surname))
                .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Name))
                .ForMember(sc => sc.CurrentProjects, o => o.MapFrom(s => s.Projects.Where(p => p.Status != ProjectStatus.COMPLETED)));
            CreateMap<Requirement, RequirementDto>()
                .ForMember(r => r.AssignedDevelopers, o => o.MapFrom(s => s.Assignees.Select(a => a.Assignee)));
            CreateMap<InitialProjectRequest, InitialProjectRequestDto>()
                .ForMember(r => r.AppointedManagerId, o => o.MapFrom(s => s.AppointedManager.Id))
                .ForMember(r => r.AppointedManagerEmail, o => o.MapFrom(s => s.AppointedManager.AppUser.Email));
            CreateMap<ProjectPhase, ProjectPhaseDto>()
                .ForMember(p => p.Requirements, o => o.MapFrom(s => s.Requirements.Where(r => r.Status == RequirementApproveStatus.APPROVED)));
            CreateMap<AppUser, UserDto>()
                .ForMember(u => u.Name, o => o.MapFrom(s => s.Name))
                .ForMember(u => u.Surname, o => o.MapFrom(s => s.Surname))
                .ForMember(u => u.Email, o => o.MapFrom(s => s.Email))
                .ForMember(u => u.ProfileImageUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(u => u.Photos, o => o.MapFrom(s => s.Photos))
                .ForMember(u => u.Role, o => o.MapFrom(s => s.Role.ToString()));
            CreateMap<SoftwareCompany, CompanyRegisterResponse>()
                .ForMember(sc => sc.Id, o => o.MapFrom(s => s.Id))
                .ForMember(sc => sc.CompanyName, o => o.MapFrom(s => s.Name))
                .ForMember(sc => sc.Address, o => o.MapFrom(s => s.Address))
                .ForMember(sc => sc.Contact, o => o.MapFrom(s => s.Contact))
                .ForMember(sc => sc.Web, o => o.MapFrom(s => s.Web))
                .ForMember(sc => sc.User, o => o.MapFrom(s => s.AppUser));

            CreateMap<Message, MessageDto>();
            CreateMap<Rating, RatingDto>();
        }
    }
}