using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectPhases
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid ProjectPhaseId { get; set; }
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
                var projectPhase = await _context.ProjectPhases
                    .Include(pp => pp.Requirements)
                    .Include(pp => pp.Project)
                    .ThenInclude(p => p.Phases)
                    .FirstOrDefaultAsync(pp => pp.Id == request.ProjectPhaseId);

                if (projectPhase == null) return null;

                foreach (var phase in projectPhase.Project.Phases)
                {
                    if (phase.SerialNumber > projectPhase.SerialNumber)
                    {
                        phase.SerialNumber--;
                    }
                }

                _context.Requirements.RemoveRange(projectPhase.Requirements);
                _context.ProjectPhases.Remove(projectPhase);

                return await _context.SaveChangesAsync() > 0 ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to delete the project phase");
            }
        }
    }
}