using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareCompanies
{
    public class Details
    {
        public class Query : IRequest<Result<SoftwareCompanyDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SoftwareCompanyDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<SoftwareCompanyDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var company = await _context.SoftwareCompanies
                    .Include(x => x.AppUser)
                    .ProjectTo<SoftwareCompanyDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (company == null) return null;

                return Result<SoftwareCompanyDto>.Success(company);
            }
        }
    }
}