using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Developers
{
    public class ListFreeDevelopersForProjectTasks
    {
        public class Query : IRequest<Result<List<DeveloperDto>>>
        {
            public Guid ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<DeveloperDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<DeveloperDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var developers = await _context.Developers
                    .Include(d => d.AppUser)
                    .Include(d => d.AssignedTeams)
                    .Where(d => d.AssignedTeams.Any(t => t.DevelopmentTeam.Project.Id == request.ProjectId) && d.NumberOfActiveTasks < 3)
                    .ProjectTo<DeveloperDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (developers == null) return Result<List<DeveloperDto>>.Success(new List<DeveloperDto>());

                return Result<List<DeveloperDto>>.Success(developers);
            }
        }
    }
}