using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Requirements
{
    public class ListUnrated
    {
        public class Query : IRequest<Result<List<RequirementDto>>>
        {
            public Guid ProjectId { get; set; }
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
                if (await _context.SoftwareProjects.SingleOrDefaultAsync(x => x.Id == request.ProjectId) == null)
                    return null;

                var requirements = await _context.Requirements
                    .Include(x => x.Project)
                    .Where(x => x.Project.Id == request.ProjectId && x.QualityRating == null && x.Phase.Name.Equals("Done"))
                    .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                if (requirements == null) return Result<List<RequirementDto>>.Success(new List<RequirementDto>());

                return Result<List<RequirementDto>>.Success(requirements);
            }
        }
    }
}
