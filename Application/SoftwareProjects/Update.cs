using Application.Core;
using Domain.ModelsDTOs;
using MediatR;
using Persistence;

namespace Application.SoftwareProjects
{
    public class Update
    {
        public class Command : IRequest<Result<Unit>>
        {
            public SoftwareProjectDto Project { get; set; }
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
                var project = await _context.SoftwareProjects.FindAsync(request.Project.Id);

                if (project == null) return null;

                project.Name = request.Project.Name ?? project.Name;
                project.Description = request.Project.Description ?? project.Description;
                project.Status = Converters.ConvertToProjectStatus(request.Project.Status);

                _context.SoftwareProjects.Update(project);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update project");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
