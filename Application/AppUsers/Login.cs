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
    public class Login
    {
        public class Command : IRequest<Result<UserDto>>
        {
            public LoginDto LoginDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<UserDto>>
        {
            private readonly DataContext _context;
            private readonly TokenService _tokenService;
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IMapper _mapper;

            public Handler(DataContext context, TokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
            {
                _context = context;
                _tokenService = tokenService;
                _userManager = userManager;
                _signInManager = signInManager;
                _mapper = mapper;
            }

            public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.Email == request.LoginDto.Email);

                if (user == null) return Result<UserDto>.Failure("Invalid email");

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);

                if (result.Succeeded)
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    userDto.Token = _tokenService.CreateToken(user);
                    return Result<UserDto>.Success(userDto);
                }
       
                return Result<UserDto>.Failure("Invalid password");
            }
        }


    }
}
