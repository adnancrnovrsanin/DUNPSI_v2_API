using Application.Core;
using Application.SoftwareProjects.DTOs.Dashboard;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Dashboard
{
    public class GetManagerDashboard
    {
        public class Query : IRequest<Result<SoftwareProjectDashboardDto>>
        {
            public Guid ManagerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SoftwareProjectDashboardDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<SoftwareProjectDashboardDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var project = await _context.SoftwareProjects
                    .Include(p => p.AssignedTeam)
                    .Where(p => p.AssignedTeam.ProjectManagerId == request.ManagerId && p.Status != ProjectStatus.COMPLETED)
                    .ProjectTo<SoftwareProjectDashboardDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                if (project == null) return Result<SoftwareProjectDashboardDto>.Failure("Project not found");

                return Result<SoftwareProjectDashboardDto>.Success(project);
            }
        }
    }
}
