using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class ListManagerRequests
    {
        public class Query : IRequest<Result<List<InitialProjectRequestDto>>>
        {
            public Guid ProjectManagerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<InitialProjectRequestDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<Result<List<InitialProjectRequestDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                ProjectManagerDto manager = await _context.ProjectManagers
                    .ProjectTo<ProjectManagerDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.ProjectManagerId);

                if (manager == null) return Result<List<InitialProjectRequestDto>>.Failure("Manager not found");

                List<InitialProjectRequestDto> requests = await _context.InitialProjectRequests
                    .Where(x => x.AppointedManagerId == request.ProjectManagerId && x.RejectedByManager == false)
                    .ProjectTo<InitialProjectRequestDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<InitialProjectRequestDto>>.Success(requests);
            }
        }
    }
}
