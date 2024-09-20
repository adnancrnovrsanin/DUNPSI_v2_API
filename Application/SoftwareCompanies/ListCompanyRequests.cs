using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareCompanies
{
    public class ListCompanyRequests
    {
        public class Query : IRequest<Result<List<InitialProjectRequestDto>>>
        {
            public Guid CompanyId { get; set; }
        }

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
                var requests = await _context.InitialProjectRequests
                    .Include(x => x.Client)
                    .Where(x => x.Client.Id == request.CompanyId)
                    .ProjectTo<InitialProjectRequestDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (requests == null) return Result<List<InitialProjectRequestDto>>.Success(new List<InitialProjectRequestDto>());

                return Result<List<InitialProjectRequestDto>>.Success(requests);
            }
        }
    }
}
