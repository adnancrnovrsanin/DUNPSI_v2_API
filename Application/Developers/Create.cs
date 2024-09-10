using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Persistence;

namespace Application.Developers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public DeveloperDto Developer { get; set; }
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
                var user = await _context.Users.FindAsync(request.Developer.AppUserId);

                if (user == null) return null;

                var newDeveloper = new Developer
                {
                    AppUserId = request.Developer.AppUserId,
                    AppUser = user,
                    Position = request.Developer.Position,
                    NumberOfActiveTasks = 0
                };

                _context.Developers.Add(newDeveloper);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create developer");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}