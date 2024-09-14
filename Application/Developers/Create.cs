using Application.Core;
using Application.Developers.DTOs;
using Application.SoftwareCompanies.DTOs;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Developers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public DeveloperRegisterRequest Developer { get; set; }
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
                if (await _userManager.Users.AnyAsync(x => x.Email == request.Developer.User.Email))
                    return Result<Unit>.Failure("User already exists");

                var user = new AppUser
                {
                    Name = request.Developer.User.Name,
                    Surname = request.Developer.User.Surname,
                    Email = request.Developer.User.Email,
                    UserName = request.Developer.User.Email,
                    Role = Role.DEVELOPER,
                    Photos = []
                };

                var result = await _userManager.CreateAsync(user, request.Developer.User.Password);
                if (!result.Succeeded) return Result<Unit>.Failure("Problem registering developer");

                var newDeveloper = new Developer
                {
                    AppUserId = user.Id,
                    AppUser = user,
                    Position = request.Developer.Position,
                    NumberOfActiveTasks = 0
                };

                _context.Developers.Add(newDeveloper);

                var result2 = await _context.SaveChangesAsync() > 0;
                if (!result2) return Result<Unit>.Failure("Failed to create developer");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}