using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Developers
{
    public class Details
    {
        public class Query : IRequest<Result<DeveloperDto>>
        {
            public string AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DeveloperDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DeveloperDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var developer = await _context.Developers
                    .Include(x => x.AppUser)
                    .ProjectTo<DeveloperDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.AppUserId == request.AppUserId);
                
                if (developer == null) return null;

                return Result<DeveloperDto>.Success(developer);
            }
        }
    }
}