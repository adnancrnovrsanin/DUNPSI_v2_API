using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareCompanies
{
    public class ListClientProjectsActionNeeded
    {
        public class Query : IRequest<Result<List<SoftwareProjectDto>>>
        {
            public Guid ClientId { get; set; }
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
                    .Include(x => x.Client)
                    .Where(x => x.Client.Id == request.ClientId && x.Status == ProjectStatus.WAITING_CLIENT_INPUT)
                    .ProjectTo<SoftwareProjectDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (projects == null) return Result<List<SoftwareProjectDto>>.Success(new List<SoftwareProjectDto>());

                return Result<List<SoftwareProjectDto>>.Success(projects);
            }
        }
    }
}
