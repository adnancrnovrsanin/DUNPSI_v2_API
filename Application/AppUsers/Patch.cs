using Application.AppUsers.DTOs;
using Application.Core;
using Domain.ModelDTOs;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Data;
using AutoMapper;

namespace Application.AppUsers
{
    public class Patch
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string UserId { get; set; }
            public UserPatchRequest UserPatch { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;

            public Handler(DataContext context, TokenService tokenService, UserManager<AppUser> userManager, IMapper mapper)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
                if (user == null)
                    return Result<Unit>.Failure("User not found");

                switch (user.Role)
                {
                    case Role.PRODUCT_MANAGER:
                        var productManager = await _context.ProductManagers.FirstOrDefaultAsync(x => x.AppUserId == user.Id, cancellationToken);
                        if (productManager != null)
                        {
                            _context.ProductManagers.Remove(productManager);
                        }
                        break;

                    case Role.PROJECT_MANAGER:
                        var projectManager = await _context.ProjectManagers.FirstOrDefaultAsync(x => x.AppUserId == user.Id, cancellationToken);
                        if (projectManager != null)
                        {
                            _context.ProjectManagers.Remove(projectManager);
                        }
                        break;

                    case Role.DEVELOPER:
                        var developer = await _context.Developers.FirstOrDefaultAsync(x => x.AppUserId == user.Id, cancellationToken);
                        if (developer != null)
                        {
                            _context.Developers.Remove(developer);
                        }
                        break;

                    default:
                        return Result<Unit>.Failure("Invalid role");
                }

                if (Enum.TryParse<Role>(request.UserPatch.Role, true, out var parsedRole))
                    user.Role = parsedRole;
                else
                    return Result<Unit>.Failure("Invalid role specified");

                switch (user.Role)
                {
                    case Role.PRODUCT_MANAGER:
                        var productManager = new ProductManager
                        {
                            AppUserId = user.Id,
                            AppUser = user
                        };
                        _context.ProductManagers.Add(productManager);
                        break;

                    case Role.PROJECT_MANAGER:
                        var projectManager = new ProjectManager
                        {
                            AppUserId = user.Id,
                            AppUser = user
                        };
                        _context.ProjectManagers.Add(projectManager);
                        break;

                    case Role.DEVELOPER:
                        var developer = new Developer
                        {
                            AppUserId = user.Id,
                            AppUser = user
                        };
                        _context.Developers.Add(developer);
                        break;

                    default:
                        return Result<Unit>.Failure("Invalid role");
                }

                // Save the changes asynchronously
                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                    return Result<Unit>.Failure("Failed to delete user from the appropriate context");

                return Result<Unit>.Success(Unit.Value);
            }
        }


    }
}
