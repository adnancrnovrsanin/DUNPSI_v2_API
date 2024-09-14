using Application.Core;
using Application.SoftwareProjects.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class GetRequirementsForApproval
    {
        public class Query : IRequest<Result<List<RequirementDto>>>
        {
            public RequirementsRequestParams Params { get; set; }
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
                var requirementsList = new List<RequirementDto>();
                var requirements = _context.Requirements
                    .Include(r => r.Project)
                    .Where(r => r.ProjectId == request.Params.ProjectId)
                    .AsQueryable();
                
                switch(request.Params.Status)
                {
                    case "WAITING_PRODUCT_MANAGER_CHANGES":
                        requirementsList = await requirements.Where(r => r.Status == RequirementApproveStatus.WAITING_PRODUCT_MANAGER_CHANGES)
                            .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
                        break;
                    case "WAITING_PROJECT_MANAGER_CHANGES":
                        requirementsList = await requirements.Where(r => r.Status == RequirementApproveStatus.WAITING_PROJECT_MANAGER_CHANGES)
                            .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
                        break;
                    case "WAITING_PRODUCT_MANAGER_APPROVAL":
                        requirementsList = await requirements.Where(r => r.Status == RequirementApproveStatus.WAITING_PRODUCT_MANAGER_APPROVAL)
                            .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
                        break;
                    case "WAITING_PROJECT_MANAGER_APPROVAL":
                        requirementsList = await requirements.Where(r => r.Status == RequirementApproveStatus.WAITING_PROJECT_MANAGER_APPROVAL)
                            .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
                        break;
                    case "APPROVED":
                        requirementsList = await requirements.Where(r => r.Status == RequirementApproveStatus.APPROVED)
                            .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
                        break;
                    case "REJECTED":
                        requirementsList = await requirements.Where(r => r.Status == RequirementApproveStatus.REJECTED)
                            .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
                        break;
                    case "CHANGES_REQUIRED":
                        requirementsList = await requirements.Where(r => r.Status == RequirementApproveStatus.CHANGES_REQUIRED)
                            .ProjectTo<RequirementDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
                        break;
                    default:
                        break;
                }

                return Result<List<RequirementDto>>.Success(requirementsList);
            }
        }
    }
}