using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.SoftwareCompanies.DTOs;
using Application.Services;
using AutoMapper;
using Domain.ModelDTOs;

namespace Application.SoftwareCompanies
{
    public class Create
    {
        public class Command : IRequest<Result<CompanyRegisterResponse>>
        {
            public CompanyRegisterRequest SoftwareCompany { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<CompanyRegisterResponse>>
        {
            private readonly DataContext _context;
            private readonly TokenService _tokenService;
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;

            public Handler(DataContext context, TokenService tokenService, UserManager<AppUser> userManager, IMapper mapper)
            {
                _context = context;
                _tokenService = tokenService;
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Result<CompanyRegisterResponse>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == request.SoftwareCompany.User.Email))
                    return Result<CompanyRegisterResponse>.Failure("Software company with this user already exists");

                var user = new AppUser
                {
                    Name = request.SoftwareCompany.User.Name,
                    Surname = request.SoftwareCompany.User.Surname,
                    Email = request.SoftwareCompany.User.Email,
                    UserName = request.SoftwareCompany.User.Email,
                    Role = Role.SOFTWARE_COMPANY,
                    Photos = []
                };

                var result = await _userManager.CreateAsync(user, request.SoftwareCompany.User.Password);
                if (!result.Succeeded) return Result<CompanyRegisterResponse>.Failure("Problem registering software company");

                if (await _context.SoftwareCompanies.Include(sc => sc.AppUser).AnyAsync(x => x.AppUser.Email == request.SoftwareCompany.User.Email))
                    return Result<CompanyRegisterResponse>.Failure("Software company with this user already exists");

                var softwareCompany = new SoftwareCompany
                {
                    Name = request.SoftwareCompany.CompanyName,
                    Address = request.SoftwareCompany.Address,
                    Contact = request.SoftwareCompany.Contact,
                    Web = request.SoftwareCompany.Web,
                    AppUser = user
                };

                _context.SoftwareCompanies.Add(softwareCompany);

                var result2 = await _context.SaveChangesAsync() > 0;

                if (!result2) return Result<CompanyRegisterResponse>.Failure("Problem registering software company");

                var softwareCompanyResponse = _mapper.Map<CompanyRegisterResponse>(softwareCompany);
                softwareCompanyResponse.User.Token = _tokenService.CreateToken(user);

                return Result<CompanyRegisterResponse>.Success(softwareCompanyResponse);
            }
        }
    }
}