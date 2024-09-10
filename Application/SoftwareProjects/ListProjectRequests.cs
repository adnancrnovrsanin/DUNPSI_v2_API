using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class ListProjectRequests
    {
        public class Query : IRequest<Result<List<InitialProjectRequestDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<InitialProjectRequestDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<InitialProjectRequestDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var projectRequests = await _context.InitialProjectRequests
                    .Where(x => !x.Rejected && (x.AppointedManager == null || (x.AppointedManager != null && x.RejectedByManager == true)))
                    .ProjectTo<InitialProjectRequestDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                if (projectRequests == null) return Result<List<InitialProjectRequestDto>>.Success(new List<InitialProjectRequestDto>());

                return Result<List<InitialProjectRequestDto>>.Success(projectRequests);
            }
        }
    }
}