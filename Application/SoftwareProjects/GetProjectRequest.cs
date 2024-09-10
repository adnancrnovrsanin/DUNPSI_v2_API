using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class GetProjectRequest
    {
        public class Query : IRequest<Result<InitialProjectRequestDto>>
        {
            public Guid ProjectRequestId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<InitialProjectRequestDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<InitialProjectRequestDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var projectRequest = await _context.InitialProjectRequests
                    .Include(x => x.Client)
                    .ProjectTo<InitialProjectRequestDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.Id == request.ProjectRequestId);

                if (projectRequest == null) return null;

                return Result<InitialProjectRequestDto>.Success(projectRequest);
            }
        }
    }
}