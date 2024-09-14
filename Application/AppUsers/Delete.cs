using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Persistence;

namespace Application.AppUsers
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string AppUserId { get; set; }
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
                var user = await _context.Users.FindAsync(request.AppUserId);

                if (user == null) return null;

                _context.Users.Remove(user);
                _context.SaveChanges();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}