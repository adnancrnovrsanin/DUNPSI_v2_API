using Application.Core;
using Application.ProductManagers.DTOs;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProductManagers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProductManagerRegisterRequest ProductManager { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;

            public Handler(DataContext context, UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == request.ProductManager.User.Email))
                    return Result<Unit>.Failure("User already exists");

                var user = new AppUser
                {
                    Name = request.ProductManager.User.Name,
                    Surname = request.ProductManager.User.Surname,
                    Email = request.ProductManager.User.Email,
                    UserName = request.ProductManager.User.Email,
                    Role = Role.PRODUCT_MANAGER,
                    Photos = []
                };

                var result = await _userManager.CreateAsync(user, request.ProductManager.User.Password);
                if (!result.Succeeded) return Result<Unit>.Failure("Problem registering developer");

                var newProductManager = new ProductManager
                {
                    AppUserId = user.Id,
                    AppUser = user
                };

                _context.ProductManagers.Add(newProductManager);

                var result1 = await _context.SaveChangesAsync() > 0;
                if (!result1) return Result<Unit>.Failure("Failed to create product manager");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}