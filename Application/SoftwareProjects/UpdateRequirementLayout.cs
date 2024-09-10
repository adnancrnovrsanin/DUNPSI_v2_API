using Application.Core;
using Application.SoftwareProjects.DTOs;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class UpdateRequirementLayout
    {
        public class Command : IRequest<Result<Unit>>
        {
            public UpdateRequirementLayoutDto UpdateRequest { get; set; }
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
                var projectPhases = new List<ProjectPhase>();
                var requirements = new List<Requirement>();
                foreach (var projectPhase in request.UpdateRequest.ProjectPhases)
                {
                    var phaseDb = await _context.ProjectPhases.Include(p => p.Requirements).SingleOrDefaultAsync(p => p.Id == projectPhase.Id);
                    requirements.Clear();

                    if (phaseDb == null) return null;
                    phaseDb.Requirements.Clear();

                    foreach (var requirement in projectPhase.Requirements)
                    {
                        var reqDb = await _context.Requirements.Include(r => r.Assignees).ThenInclude(a => a.Assignee).SingleOrDefaultAsync(r => r.Id == requirement.Id);
                        if (reqDb == null) return null;
                        reqDb.PhaseId = phaseDb.Id;
                        reqDb.Phase = phaseDb;
                        reqDb.SerialNumber = requirement.SerialNumber;

                        if (phaseDb.Name == "Done") {
                            foreach(var assignee in reqDb.Assignees) {
                                assignee.Assignee.NumberOfActiveTasks--;
                            }
                        }

                        requirements.Add(reqDb);
                        phaseDb.Requirements.Add(reqDb);
                    }

                    projectPhases.Add(phaseDb);
                }

                _context.Requirements.UpdateRange(requirements);
                _context.ProjectPhases.UpdateRange(projectPhases);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update requirement layout");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}