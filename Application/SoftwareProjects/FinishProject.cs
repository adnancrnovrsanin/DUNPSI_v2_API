using Application.Core;
using MediatR;
using Persistence;

namespace Application.SoftwareProjects
{
    public class FinishProject
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid ProjectId { get; set; }
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
                var project = await _context.SoftwareProjects.FindAsync(request.ProjectId);

                if (project == null) return null;

                project.Finished = true;

                _context.SoftwareProjects.Update(project);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to finish project");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}