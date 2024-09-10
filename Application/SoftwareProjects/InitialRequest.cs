using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareProjects
{
    public class InitialRequest
    {
        public class Command : IRequest<Result<Unit>>
        {
            public InitialProjectRequestDto InitialProjectRequest { get; set; }
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
                var client = await _context.SoftwareCompanies.SingleOrDefaultAsync(x => x.Id == request.InitialProjectRequest.ClientId);

                if (client == null) return null;

                var initialProjectRequest = new InitialProjectRequest
                {
                    ProjectName = request.InitialProjectRequest.ProjectName,
                    ProjectDescription = request.InitialProjectRequest.ProjectDescription,
                    DueDate = DateTimeOffset.Parse(request.InitialProjectRequest.DueDate).UtcDateTime,
                    Rejected = false,
                    ClientId = client.Id,
                    RejectedByManager = false,
                    ManagerRejectionReason = "",
                };

                _context.InitialProjectRequests.Add(initialProjectRequest);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create initial project request");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}