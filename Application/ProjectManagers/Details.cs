using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectManagers
{
    public class Details
    {
        public class Query : IRequest<Result<ProjectManagerDto>>
        {
            public string AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ProjectManagerDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<ProjectManagerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var manager = await _context.ProjectManagers
                    .Include(x => x.AppUser)
                    .ProjectTo<ProjectManagerDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => string.Equals(x.AppUserId, request.AppUserId));

                if (manager == null) return null;

                return Result<ProjectManagerDto>.Success(manager);
            }
        }
    }
}