using Application.Core;
using Domain.ModelsDTOs;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SoftwareProjects
{
    public class RequestManager
    {
        public class Command : IRequest<Result<Unit>>
        {
            public InitialProjectRequestDto ProjectRequest { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                this._context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var projectRequest = await _context.InitialProjectRequests.FindAsync(request.ProjectRequest.Id);
                var appointedManager = await _context.ProjectManagers.FindAsync(request.ProjectRequest.AppointedManagerId);

                if (projectRequest == null || appointedManager == null) return null;

                projectRequest.AppointedManager = appointedManager;
                projectRequest.AppointedManagerId = appointedManager.Id;

                _context.InitialProjectRequests.Update(projectRequest);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to appoint manager");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
