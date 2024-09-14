using Application.ProjectManagers;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class ProjectManagerController : BaseApiController
    {
        [HttpGet("{appUserId}")]
        public async Task<IActionResult> Get(string appUserId)
        {
            return HandleResult(await Mediator.Send(new Details.Query { AppUserId = appUserId }));
        }

        [HttpGet("free-project-managers")]
        public async Task<IActionResult> GetFreeProjectManagers()
        {
            return HandleResult(await Mediator.Send(new ListFreeProjectManagers.Query()));
        }

        [HttpGet("current-project/{managerId}")]
        public async Task<IActionResult> GetCurrentProject(Guid managerId)
        {
            return HandleResult(await Mediator.Send(new CurrentProject.Query { ManagerId = managerId }));
        }

        [HttpGet("project-history/{managerId}")]
        public async Task<IActionResult> GetProjectHistory(Guid managerId)
        {
            return HandleResult(await Mediator.Send(new ProjectHistory.Query { ManagerId = managerId }));
        }
    }
}