using Application.Core;
using Application.Dashboard.DTOs;
using Application.SoftwareProjects.DTOs.Dashboard;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Dashboard
{
    public class GetCompanyDashboard
    {
        public class Query : IRequest<Result<CompanyDashboardData>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<CompanyDashboardData>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<CompanyDashboardData>> Handle(Query request, CancellationToken cancellationToken)
            {
                var allClients = await _context.SoftwareCompanies
                    .ProjectTo<SoftwareCompanyDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var allProjects = await _context.SoftwareProjects
                    .ProjectTo<SoftwareProjectDashboardDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var allProjectManagers = await _context.ProjectManagers
                    .ProjectTo<ProjectManagerDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var allDevelopers = await _context.Developers
                    .ProjectTo<DeveloperDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var companyDashboardData = new CompanyDashboardData
                {
                    AllClients = allClients,
                    AllProjects = allProjects,
                    AllProjectManagers = allProjectManagers,
                    AllDevelopers = allDevelopers
                };

                return Result<CompanyDashboardData>.Success(companyDashboardData);
            }
        }
    }
}
