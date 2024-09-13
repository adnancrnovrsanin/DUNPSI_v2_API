using System.Security.Claims;
using API.DTOs;
using Application.AppUsers.DTOs;
using Application.Services;
using CloudinaryDotNet;
using Domain;
using Domain.ModelsDTOs;
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
    public class AccountController : BaseApiController
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
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.Login.Command { LoginDto = loginDto }));
        }

        //[HttpPost("register")]
        //public async Task<ActionResult<IActionResult>> Register(RegisterDto registerDto)
        //{
        //    if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
        //    {
        //        return BadRequest("Email taken");
        //    }

        //    Role role;
        //    switch (registerDto.Role)
        //    {
        //        case "PRODUCT_MANAGER":
        //            role = Role.PRODUCT_MANAGER;
        //            break;
        //        case "PROJECT_MANAGER":
        //            role = Role.PROJECT_MANAGER;
        //            break;
        //        case "DEVELOPER":
        //            role = Role.DEVELOPER;
        //            break;
        //        case "SOFTWARE_COMPANY":
        //            role = Role.SOFTWARE_COMPANY;
        //            break;
        //        default:
        //            ModelState.AddModelError("role", "Invalid role");
        //            return ValidationProblem();
        //    }

        //    var user = new AppUser
        //    {
        //        Name = registerDto.Name,
        //        Surname = registerDto.Surname,
        //        Email = registerDto.Email,
        //        UserName = registerDto.Email,
        //        Role = role,
        //        Photos = new List<Photo>()
        //    };

        //    var result = await _userManager.CreateAsync(user, registerDto.Password);

        //    if (result.Succeeded)
        //    {
        //        return CreateUserObject(user);
        //    }

        //    return BadRequest("Problem registering user");
        //}

        //[HttpPost("register-company")]
        //public async Task<ActionResult<CompanyRegisterResponse>> RegisterCompany(CompanyRegisterRequest registerRequest)
        //{
        //    if (await _userManager.Users.AnyAsync(x => x.Email == registerRequest.Email))
        //    {
        //        return BadRequest("User with this email already exists");
        //    }

        //    var user = new AppUser
        //    {
        //        Name = registerRequest.Name,
        //        Surname = registerRequest.Surname,
        //        Email = registerRequest.Email,
        //        UserName = registerRequest.Email,
        //        Role = Role.SOFTWARE_COMPANY,
        //        Photos = new List<Photo>()
        //    };

        //    var result = await _userManager.CreateAsync(user, registerRequest.Password);

        //    if (!result.Succeeded) return BadRequest("Problem registering user");

        //    if (await _context.SoftwareCompanies.Include(sc => sc.AppUser).AnyAsync(x => x.AppUser.Email == registerRequest.Email))
        //        return BadRequest("Software company with this user already exists");

        //    var softwareCompany = new SoftwareCompany
        //    {
        //        Name = registerRequest.CompanyName,
        //        Address = registerRequest.Address,
        //        Contact = registerRequest.Contact,
        //        Web = registerRequest.Web,
        //        AppUser = user
        //    };

        //    _context.SoftwareCompanies.Add(softwareCompany);

        //    var result2 = await _context.SaveChangesAsync() > 0;

        //    if (!result2) return BadRequest("Problem registering software company");

        //    return Ok(
        //        new CompanyRegisterResponse
        //        {
        //            User = CreateUserObject(user),
        //            Id = softwareCompany.Id,
        //            CompanyName = softwareCompany.Name,
        //            Address = softwareCompany.Address,
        //            Contact = softwareCompany.Contact,
        //            Web = softwareCompany.Web
        //        }
        //    );
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
           return HandleResult(await Mediator.Send(new Application.AppUsers.GetUserById.Query { Id = id }));
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.GetUserByEmail.Query { Email = email }));
        }

        [HttpGet("search/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.GetAllUsers.Query()));
        }

        [HttpGet("search/{search}")]
        public async Task<IActionResult> SearchUsers(string search)
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.GetBySearchQuery.Query { SearchQuery = search }));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.GetCurrentUser.Query { Email = User.FindFirstValue(ClaimTypes.Email)}));
        }
    }
}