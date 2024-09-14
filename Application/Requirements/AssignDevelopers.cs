using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Requirements
{
    public class AssignDevelopers
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
                var requirement = await _context.Requirements.Include(r => r.Assignees).SingleOrDefaultAsync(r => r.Id == request.Requirement.Id);

                if (requirement == null) return null;

                var developers = new List<RequirementManagement>();

                foreach (var developer in request.Requirement.AssignedDevelopers)
                {
                    var dev = await _context.Developers.FindAsync(developer.Id);

                    if (dev == null) return null;

                    if (dev.NumberOfActiveTasks >= 3) return Result<Unit>.Failure("Developer has too many active tasks");

                    if (requirement.Assignees.Any(a => a.AssigneeId == dev.Id)) continue;

                    var placement = new RequirementManagement
                    {
                        Id = Guid.NewGuid(),
                        AssigneeId = dev.Id,
                        RequirementId = requirement.Id,
                        Assignee = dev,
                        Requirement = requirement
                    };

                    dev.NumberOfActiveTasks++;
                    developers.Add(placement);
                }

                _context.Developers.UpdateRange(developers.Select(d => d.Assignee));
                _context.RequirementManagements.AddRange(developers);

                requirement.Assignees = developers;
                _context.Requirements.Update(requirement);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to assign developers to requirement");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}