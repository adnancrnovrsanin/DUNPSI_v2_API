using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Persistence;

namespace Application.ProductManagers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProductManagerDto ProductManager { get; set; }
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
                var user = await _context.Users.FindAsync(request.ProductManager.AppUserId);

                if (user == null) return null;

                var newProductManager = new ProductManager
                {
                    AppUserId = request.ProductManager.AppUserId,
                    AppUser = user
                };

                _context.ProductManagers.Add(newProductManager);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create product manager");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}