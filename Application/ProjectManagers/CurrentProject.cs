using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectManagers
{
    public class CurrentProject
    {
        public class Query : IRequest<Result<SoftwareProjectDto>>
        {
            public Guid ManagerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SoftwareProjectDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<SoftwareProjectDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var project = await _context.SoftwareProjects
                    .Include(sp => sp.AssignedTeam)
                    .ProjectTo<SoftwareProjectDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(sp => sp.AssignedTeam.Manager.Id == request.ManagerId && sp.Finished == false);

                return Result<SoftwareProjectDto>.Success(project);
            }
        }
    }
}