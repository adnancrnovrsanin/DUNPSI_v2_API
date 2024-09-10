using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Persistence;

namespace Application.ProjectManagers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProjectManagerDto ProjectManager { get; set; }
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
                var user = await _context.Users.FindAsync(request.ProjectManager.AppUserId);

                if (user == null) return null;

                var newProjectManager = new ProjectManager
                {
                    AppUserId = request.ProjectManager.AppUserId,
                    AppUser = user,
                    CertificateUrl = request.ProjectManager.CertificateUrl,
                    YearsOfExperience = request.ProjectManager.YearsOfExperience
                };

                _context.ProjectManagers.Add(newProjectManager);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create project manager");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}