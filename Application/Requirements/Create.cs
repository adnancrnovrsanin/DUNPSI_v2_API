using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Requirements
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public RequirementDto Requirement { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var project = await _context.SoftwareProjects
                    .Include(x => x.Phases)
                    .ThenInclude(x => x.Requirements)
                    .SingleOrDefaultAsync(x => x.Id == request.Requirement.ProjectId);
                var requirementPhase = project.Phases.SingleOrDefault(x => x.Name == "Requirements Analysis");


                if (project == null) return null;

                var requirement = new Requirement
                {
                    Name = request.Requirement.Name,
                    Description = request.Requirement.Description,
                    Status = Converters.ConvertToRequirementApproveStatus(request.Requirement.Status),
                    ProjectId = request.Requirement.ProjectId,
                    PhaseId = requirementPhase.Id,
                    Project = project,
                    Phase = requirementPhase,
                    SerialNumber = requirementPhase.Requirements.Count + 1,
                    IdNumber = project.Phases.Sum(p => p.Requirements.Count) + 1,
                    Type = Converters.ConvertToRequirementType(request.Requirement.Type),
                    Priority = Converters.ConvertToRequirementPriority(request.Requirement.Priority),
                    Assignees = new List<RequirementManagement>(),
                    DeveloperRatings = new List<Rating>()
                };

                _context.Requirements.UpdateRange(requirementPhase.Requirements);
                
                _context.Requirements.Add(requirement);
                requirementPhase.Requirements.Add(requirement);


                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create requirement");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}