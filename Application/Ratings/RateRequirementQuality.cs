using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Ratings
{
    public class RateRequirementQuality
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
                var projectManager = await _context.ProjectManagers.Include(pm => pm.AppUser).SingleOrDefaultAsync(x => x.Id.ToString().Equals(request.RatingDto.ProjectManagerId));
                var requirement = await _context.Requirements.Include(r => r.Assignees).SingleOrDefaultAsync(x => x.Id.ToString().Equals(request.RatingDto.RequirementId));

                if (projectManager == null || requirement == null)
                    return Result<Unit>.Failure("Project manager or requirement not found");

                var rating = new Rating
                {
                    RequirementId = requirement.Id,
                    Requirement = requirement,
                    ProjectManager = projectManager,
                    ProjectManagerId = projectManager.Id,
                    RatingValue = request.RatingDto.RatingValue,
                    Comment = request.RatingDto.Comment,
                    DateTimeRated = DateTime.UtcNow
                };

                var now = DateTime.UtcNow;

                var timeDifferenceRequirement = now.Millisecond - requirement.CreatedAt.Millisecond;

                foreach (var rm in requirement.Assignees)
                {
                    var dev = rm.Assignee;
                    var timeDifferenceAssignee = now.Millisecond - rm.CreatedAt.Millisecond;

                    if ((double)timeDifferenceAssignee / timeDifferenceRequirement >= 0.25)
                    {
                        dev.QualityRating = (dev.QualityRating * dev.RatingCount + request.RatingDto.RatingValue) / ++dev.RatingCount;
                        _context.Developers.Update(dev);
                    }
                }

                _context.Ratings.Add(rating);

                return await _context.SaveChangesAsync() > 0 ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to rate the developer");
            }
        }
    }
}