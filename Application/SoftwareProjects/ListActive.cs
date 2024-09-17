using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class ListActive
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
                var allProjects = await _context.SoftwareProjects
                    .Where(sp => sp.Status != ProjectStatus.COMPLETED)
                    .ToListAsync();

                var allProjectsDto = _mapper.Map<List<SoftwareProject>, List<SoftwareProjectDto>>(allProjects);

                return Result<List<SoftwareProjectDto>>.Success(allProjectsDto);
            }
        }
    }
}
