using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectManagers
{
    public class ListFreeProjectManagers
    {
        public class Query : IRequest<Result<List<ProjectManagerDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<ProjectManagerDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<ProjectManagerDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var managers = await _context.ProjectManagers
                    .Include(pm => pm.AppUser)
                    .Include(pm => pm.ManagedTeams)
                    .Where(pm => !pm.ManagedTeams.Any(t => t.Project.Finished == false))
                    .ProjectTo<ProjectManagerDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                
                if (managers == null) return Result<List<ProjectManagerDto>>.Success(new List<ProjectManagerDto>());

                return Result<List<ProjectManagerDto>>.Success(managers);
            }
        }

    }
}