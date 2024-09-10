using Application.Developers;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DeveloperController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(DeveloperDto developerDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Developer = developerDto }));
        }

        [HttpGet("{appUserId}")]
        public async Task<IActionResult> Get(string appUserId)
        {
            return HandleResult(await Mediator.Send(new Details.Query { AppUserId = appUserId }));
        }

        [HttpGet("free-developers")]
        public async Task<IActionResult> GetFreeDevelopers()
        {
            return HandleResult(await Mediator.Send(new ListFreeDevelopers.Query()));
        }

        [HttpGet("free-developers/{projectId}")]
        public async Task<IActionResult> GetFreeDevelopersForProjectTasks(Guid projectId)
        {
            return HandleResult(await Mediator.Send(new ListFreeDevelopersForProjectTasks.Query { ProjectId = projectId }));
        }

        [HttpGet("current-project/{developerId}")]
        public async Task<IActionResult> GetCurrentProject(Guid developerId)
        {
            return HandleResult(await Mediator.Send(new CurrentProject.Query { DeveloperId = developerId }));
        }
    }
}