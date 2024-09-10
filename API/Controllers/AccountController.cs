using System.Security.Claims;
using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly DataContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return Unauthorized("Invalid email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return Unauthorized("Invalid password");
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email taken");
            }

            Role role;
            switch (registerDto.Role)
            {
                case "PRODUCT_MANAGER":
                    role = Role.PRODUCT_MANAGER;
                    break;
                case "PROJECT_MANAGER":
                    role = Role.PROJECT_MANAGER;
                    break;
                case "DEVELOPER":
                    role = Role.DEVELOPER;
                    break;
                case "SOFTWARE_COMPANY":
                    role = Role.SOFTWARE_COMPANY;
                    break;
                default:
                    ModelState.AddModelError("role", "Invalid role");
                    return ValidationProblem();
            }

            var user = new AppUser
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Role = role,
                Photos = new List<Photo>()
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return BadRequest("Problem registering user");
        }

        [HttpPost("register-company")]
        public async Task<ActionResult<CompanyRegisterResponse>> RegisterCompany(CompanyRegisterRequest registerRequest)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerRequest.Email))
            {
                return BadRequest("User with this email already exists");
            }

            var user = new AppUser
            {
                Name = registerRequest.Name,
                Surname = registerRequest.Surname,
                Email = registerRequest.Email,
                UserName = registerRequest.Email,
                Role = Role.SOFTWARE_COMPANY,
                Photos = new List<Photo>()
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded) return BadRequest("Problem registering user");

            if (await _context.SoftwareCompanies.Include(sc => sc.AppUser).AnyAsync(x => x.AppUser.Email == registerRequest.Email))
                return BadRequest("Software company with this user already exists");

            var softwareCompany = new SoftwareCompany
            {
                Name = registerRequest.CompanyName,
                Address = registerRequest.Address,
                Contact = registerRequest.Contact,
                Web = registerRequest.Web,
                AppUser = user
            };

            _context.SoftwareCompanies.Add(softwareCompany);

            var result2 = await _context.SaveChangesAsync() > 0;

            if (!result2) return BadRequest("Problem registering software company");

            return Ok(
                new CompanyRegisterResponse
                {
                    User = CreateUserObject(user),
                    Id = softwareCompany.Id,
                    CompanyName = softwareCompany.Name,
                    Address = softwareCompany.Address,
                    Contact = softwareCompany.Contact,
                    Web = softwareCompany.Web
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var user = await _userManager.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) return NotFound();

            return CreateUserObject(user);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userManager.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return NotFound();

            return CreateUserObject(user);
        }

        [HttpGet("search/all")]
        public async Task<ActionResult<UserDto>> GetAllUsers()
        {
            List<AppUser> users = await _userManager.Users.Include(u => u.Photos).ToListAsync();

            if (users == null) return NotFound();

            return Ok(users.Select(user => CreateUserObject(user)).ToList());
        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<List<UserDto>>> SearchUsers(string search)
        {
            var users = await _userManager.Users.Include(u => u.Photos).Where(x =>
                x.Name.ToLower().Contains(search.ToLower()) ||
                x.Surname.ToLower().Contains(search.ToLower()) ||
                x.Email.ToLower().Contains(search.ToLower())
            ).ToListAsync();

            if (users == null) return NotFound();

            return users.Select(user => CreateUserObject(user)).ToList();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Role = user.Role.ToString(),
                ProfileImageUrl = (user.Photos.Count > 0) ? user.Photos.FirstOrDefault(x => x.IsMain)?.Url : null,
                Photos = user.Photos,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}