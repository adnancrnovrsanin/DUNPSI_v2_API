using Application.SoftwareCompanies;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class SoftwareCompanyController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(SoftwareCompanyDto softwareCompanyDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { SoftwareCompany = softwareCompanyDto }));
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetSoftwareCompany(Guid companyId)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = companyId }));
        }

        [HttpGet("projects/{clientId}")]
        public async Task<IActionResult> GetClientProjects(Guid clientId)
        {
            return HandleResult(await Mediator.Send(new ListClientProjects.Query { ClientId = clientId }));
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetSoftwareCompanyByEmail(string email)
        {
            return HandleResult(await Mediator.Send(new DetailsByEmail.Query { Email = email }));
        }
    }
}