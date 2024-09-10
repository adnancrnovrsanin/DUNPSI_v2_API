using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Ratings
{
    public class RateDeveloper
    {
        public class Command : IRequest<Result<Unit>>
        {
            public RatingDto RatingDto { get; set; }
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
                    .Include(sp => sp.AssignedTeam)
                    .ThenInclude(at => at.Manager)
                    .Include(sp => sp.AssignedTeam.AssignedDevelopers)
                    .ThenInclude(ad => ad.Developer)
                    .ThenInclude(d => d.ReceivedRatings)
                    .FirstOrDefaultAsync(sp => sp.Id == Guid.Parse(request.RatingDto.ProjectId));

                if (project == null) return null;

                var developer = project.AssignedTeam.AssignedDevelopers.FirstOrDefault(d => d.Developer.Id == Guid.Parse(request.RatingDto.DeveloperId)).Developer;

                if (developer == null) return null;

                var requirement = project.Phases.SelectMany(pp => pp.Requirements).FirstOrDefault(r => r.Id == Guid.Parse(request.RatingDto.RequirementId));

                if (requirement == null) return null;

                var rating = new Rating
                {
                    RequirementId = requirement.Id,
                    Requirement = requirement,
                    ProjectManager = project.AssignedTeam.Manager,
                    ProjectManagerId = project.AssignedTeam.Manager.Id,
                    Developer = developer,
                    DeveloperId = developer.Id,
                    RatingValue = request.RatingDto.RatingValue,
                    Comment = request.RatingDto.Comment,
                    DateTimeRated = DateTime.UtcNow
                };

                developer.ReceivedRatings.Add(rating);
                _context.Ratings.Add(rating);
                _context.Developers.Update(developer);

                return await _context.SaveChangesAsync() > 0 ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to rate the developer");
            }
        }
    }
}