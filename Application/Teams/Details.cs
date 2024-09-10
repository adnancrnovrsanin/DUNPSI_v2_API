using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Teams
{
    public class Details
    {
        public class Query : IRequest<Result<TeamDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TeamDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<TeamDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var team = await _context.Teams
                    .Include(t => t.AssignedDevelopers)
                    .ThenInclude(dt => dt.Developer)
                    .Include(t => t.Manager)
                    .ProjectTo<TeamDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(t => t.Id == request.Id);

                if (team == null) return null;

                return Result<TeamDto>.Success(team);
            }
        }
    }
}