using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectManagers
{
    public class ProjectHistory
    {
        public class Query : IRequest<Result<List<SoftwareProjectDto>>>
        {
            public Guid ManagerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<SoftwareProjectDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<SoftwareProjectDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var projects = await _context.SoftwareProjects
                    .Include(sp => sp.AssignedTeam)
                    .Where(sp => sp.Finished == true && sp.AssignedTeam.ProjectManagerId == request.ManagerId)
                    .ProjectTo<SoftwareProjectDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<SoftwareProjectDto>>.Success(projects);
            }
        }
    }
}