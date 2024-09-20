using Application.SoftwareCompanies;
using Application.SoftwareCompanies.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class SoftwareCompanyController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CompanyRegisterRequest companyRegisterRequest)
        {
            return HandleResult(await Mediator.Send(new Create.Command { SoftwareCompany = companyRegisterRequest }));
        }

        [HttpGet("{companyId}/requests")]
        public async Task<IActionResult> GetCompanyRequests(Guid companyId)
        {
            return HandleResult(await Mediator.Send(new ListCompanyRequests.Query { CompanyId = companyId }));
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetSoftwareCompany(Guid companyId)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = companyId }));
        }

        [HttpGet("projects/{clientId}/action-needed")]
        public async Task<IActionResult> GetClientProjectsActionNeeded(Guid clientId)
        {
            return HandleResult(await Mediator.Send(new ListClientProjectsActionNeeded.Query { ClientId = clientId }));
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