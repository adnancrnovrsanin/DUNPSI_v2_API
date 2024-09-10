using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Requirements
{
    public class ListByUser
    {
        public class Query : IRequest<Result<List<RequirementDto>>>
        {
            public string AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<RequirementDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<RequirementDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == request.AppUserId);

                if (user == null) return null;

                if (user.Role == Role.DEVELOPER) {
                    var requirements = await _context.Requirements
                        .Include(x => x.Assignees)
                        .ThenInclude(x => x.Assignee)
                        .Where(x => x.Assignees.Any(x => x.Assignee.AppUserId == request.AppUserId))
                        .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                    
                    if (requirements == null) return Result<List<RequirementDto>>.Success(new List<RequirementDto>());

                    return Result<List<RequirementDto>>.Success(requirements);
                }

                if (user.Role == Role.PROJECT_MANAGER) {
                    var requirements = await _context.Requirements
                        .Include(x => x.Project)
                        .ThenInclude(x => x.AssignedTeam)
                        .ThenInclude(x => x.Manager)
                        .Where(x => x.Project.AssignedTeam.Manager.AppUserId == request.AppUserId)
                        .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                    
                    if (requirements == null) return Result<List<RequirementDto>>.Success(new List<RequirementDto>());

                    return Result<List<RequirementDto>>.Success(requirements);
                }

                return Result<List<RequirementDto>>.Failure("User role not found");
            }
        }
    }
}