using Application.Core;
using AutoMapper;
using Domain;
using Domain.ModelDTOs;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppUsers
{
    public class GetCurrentUser
    {
        public class Query : IRequest<Result<UserDto>> {
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;

            public Handler(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

                var userDto = _mapper.Map<UserDto>(user);
                return Result<UserDto>.Success(userDto);                
            }
        }
    }
}
