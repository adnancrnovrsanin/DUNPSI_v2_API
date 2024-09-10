using Application.Core;
using Domain;
using Domain.ModelsDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.SoftwareCompanies
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public SoftwareCompanyDto SoftwareCompany { get; set; }
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
                if (await _context.SoftwareCompanies.AnyAsync(x => x.AppUserId == request.SoftwareCompany.AppUserId))
                    return Result<Unit>.Failure("Software company with this user already exists");

                var user = await _context.Users.FindAsync(request.SoftwareCompany.AppUserId);

                if (user == null) return null;

                var softwareCompany = new SoftwareCompany
                {
                    Name = request.SoftwareCompany.CompanyName,
                    Address = request.SoftwareCompany.Address,
                    Contact = request.SoftwareCompany.Contact,
                    Web = request.SoftwareCompany.Web,
                    AppUserId = request.SoftwareCompany.AppUserId,
                    AppUser = user
                };

                _context.SoftwareCompanies.Add(softwareCompany);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create software company");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}