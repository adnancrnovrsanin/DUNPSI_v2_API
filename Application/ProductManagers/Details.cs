using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProductManagers
{
    public class Details
    {
        public class Query : IRequest<Result<ProductManagerDto>>
        {
            public string AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ProductManagerDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<ProductManagerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var manager = await _context.ProductManagers
                    .Include(x => x.AppUser)
                    .ProjectTo<ProductManagerDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.AppUserId == request.AppUserId);

                if (manager == null) return null;

                return Result<ProductManagerDto>.Success(manager);
            }
        }
    }
}