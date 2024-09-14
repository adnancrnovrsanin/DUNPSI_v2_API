using Application.Core;
using Application.ProjectManagers.DTOs;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ProjectManagers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProjectManagerRegisterRequest ProjectManager { get; set; }
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
                if (await _userManager.Users.AnyAsync(x => x.Email == request.ProjectManager.User.Email))
                    return Result<Unit>.Failure("User already exists");

                var user = new AppUser
                {
                    Name = request.ProjectManager.User.Name,
                    Surname = request.ProjectManager.User.Surname,
                    Email = request.ProjectManager.User.Email,
                    UserName = request.ProjectManager.User.Email,
                    Role = Role.PROJECT_MANAGER,
                    Photos = []
                };

                var result = await _userManager.CreateAsync(user, request.ProjectManager.User.Password);
                if (!result.Succeeded) return Result<Unit>.Failure("Problem registering developer");

                var newProjectManager = new ProjectManager
                {
                    AppUserId = user.Id,
                    AppUser = user,
                    CertificateUrl = request.ProjectManager.CertificateUrl,
                    YearsOfExperience = request.ProjectManager.YearsOfExperience
                };

                _context.ProjectManagers.Add(newProjectManager);

                var result1 = await _context.SaveChangesAsync() > 0;

                if (!result1) return Result<Unit>.Failure("Failed to create project manager");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}