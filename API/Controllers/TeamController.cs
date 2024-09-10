using Application.Teams;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TeamController : BaseApiController
    {
        [HttpPost("developer-assignment")]
        public async Task<IActionResult> DeveloperAssignment(TeamCreateDto teamCreateDto) {
            return HandleResult(await Mediator.Send(new DeveloperAssignment.Command { Team = teamCreateDto }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(Guid id) {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }
    }
}