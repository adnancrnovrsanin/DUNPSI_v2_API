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
using System.Text;
using System.Threading.Tasks;

namespace Application.AppUsers
{
    public class GetAllUsers
    {
        public class Query : IRequest<Result<List<UserDto>>>
        {
            // 
        }

        public class Handler : IRequestHandler<Query, Result<List<UserDto>>>
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

            public async Task<Result<List<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<AppUser> users = await _userManager.Users.Include(u => u.Photos).ToListAsync();

                if (users == null) return Result<List<UserDto>>.Failure("No users found");

                List<UserDto> userDto = _mapper.Map<List<UserDto>>(users);
                return Result<List<UserDto>>.Success(userDto);
            }
        }
    }
}
