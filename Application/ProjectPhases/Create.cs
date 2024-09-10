using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectPhases
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProjectPhaseDto ProjectPhase { get; set; }
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
                if (request.ProjectPhase.Name == "Requirement Analysis" || request.ProjectPhase.Name == "Done" || request.ProjectPhase.SerialNumber == 0) return Result<Unit>.Failure("Cannot create this phase");

                var project = await _context.SoftwareProjects
                    .Include(sp => sp.Phases)
                    .FirstOrDefaultAsync(sp => sp.Id == request.ProjectPhase.ProjectId);

                if (project == null) return null;

                if (project.Phases.Any(p => p.Name == request.ProjectPhase.Name)) return Result<Unit>.Failure("Phase name already exists");

                if (request.ProjectPhase.SerialNumber >= project.Phases.Count) return Result<Unit>.Failure("Serial number is too big");

                var newProjectPhase = new ProjectPhase
                {
                    Name = request.ProjectPhase.Name,
                    Description = request.ProjectPhase.Description,
                    ProjectId = request.ProjectPhase.ProjectId,
                    SerialNumber = request.ProjectPhase.SerialNumber,
                    Project = project,
                    Requirements = new List<Requirement>()
                };

                foreach (var phase in project.Phases)
                {
                    if (phase.SerialNumber >= request.ProjectPhase.SerialNumber)
                    {
                        phase.SerialNumber++;
                    }
                }

                _context.ProjectPhases.UpdateRange(project.Phases);
                _context.ProjectPhases.Add(newProjectPhase);
                project.Phases.Add(newProjectPhase);
                _context.SoftwareProjects.Update(project);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create phase");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}