using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Requirements
{
    public class List
    {
        public class Query: IRequest<Result<List<RequirementDto>>>
        {
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
                var requirements = await _context.Requirements
                    .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                
                if (requirements == null) return Result<List<RequirementDto>>.Success(new List<RequirementDto>());

                return Result<List<RequirementDto>>.Success(requirements);
            }
        }
    }
}