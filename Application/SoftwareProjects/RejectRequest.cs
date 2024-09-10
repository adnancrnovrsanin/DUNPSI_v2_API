using Application.Core;
using MediatR;
using Persistence;

namespace Application.SoftwareProjects
{
    public class RejectRequest
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid ProjectRequestId { get; set; }
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
                var projectRequest = await _context.InitialProjectRequests.FindAsync(request.ProjectRequestId);

                if (projectRequest == null) return null;

                projectRequest.Rejected = true;

                _context.InitialProjectRequests.Update(projectRequest);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to reject project request");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}