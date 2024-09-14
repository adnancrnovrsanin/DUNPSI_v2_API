using System.Security.Claims;
using Application.AppUsers.DTOs;
using Application.Services;
using Application.SoftwareCompanies.DTOs;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.GetCurrentUser.Query { Email = User.FindFirstValue(ClaimTypes.Email)}));
        }
    }
}