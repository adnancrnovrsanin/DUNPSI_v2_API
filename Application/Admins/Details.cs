using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ModelDTOs;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Admins
{
    public class Details
    {
        public class Query : IRequest<Result<AdminDto>>
        {
            public string AppUserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AdminDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<AdminDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var manager = await _context.Admins
                    .Include(x => x.AppUser)
                    .ProjectTo<AdminDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.AppUserId == request.AppUserId);

                if (manager == null) return null;

                return Result<AdminDto>.Success(manager);
            }
        }
    }
}
