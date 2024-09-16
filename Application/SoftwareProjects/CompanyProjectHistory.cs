using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class CompanyProjectHistory
    {
        public class Query : IRequest<Result<List<SoftwareProjectDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<SoftwareProjectDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<SoftwareProjectDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var projects = await _context.SoftwareProjects
                    .Include(sp => sp.AssignedTeam)
                    .ProjectTo<SoftwareProjectDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<SoftwareProjectDto>>.Success(projects);
            }
        }
    }
}
