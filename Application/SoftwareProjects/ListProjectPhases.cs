using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class ListProjectPhases
    {
        public class Query : IRequest<Result<List<ProjectPhaseDto>>>
        {
            public Guid ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<ProjectPhaseDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<ProjectPhaseDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var projectPhases = await _context.ProjectPhases
                    .Include(p => p.Requirements)
                    .ThenInclude(r => r.Assignees)
                    .Where(x => x.ProjectId == request.ProjectId)
                    .ProjectTo<ProjectPhaseDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                if (projectPhases == null) return Result<List<ProjectPhaseDto>>.Success(new List<ProjectPhaseDto>());

                return Result<List<ProjectPhaseDto>>.Success(projectPhases);
            }
        }
    }
}