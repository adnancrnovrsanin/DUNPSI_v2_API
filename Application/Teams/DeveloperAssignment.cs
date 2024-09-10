using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Teams
{
    public class DeveloperAssignment
    {
        public class Command : IRequest<Result<Unit>>
        {
            public TeamCreateDto Team { get; set; }
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
                var team = await _context.Teams.FindAsync(request.Team.Id);

                if (team == null) return null;

                var developers = new List<DeveloperTeamPlacement>();

                foreach (var developer in request.Team.Developers)
                {
                    var dev = await _context.Developers.FindAsync(developer.Id);

                    if (dev == null) return null;

                    var placement = new DeveloperTeamPlacement
                    {
                        DeveloperId = dev.Id,
                        DevelopmentTeamId = team.Id,
                        Developer = dev,
                        DevelopmentTeam = team
                    };

                    developers.Add(placement);
                }

                _context.DeveloperTeamPlacements.AddRange(developers);

                team.AssignedDevelopers = developers;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to assign developers to team");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}