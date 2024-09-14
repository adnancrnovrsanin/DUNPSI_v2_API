using Application.ProjectPhases;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class ProjectPhasesController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(ProjectPhaseDto projectPhaseCreateDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { ProjectPhase = projectPhaseCreateDto }));
        }      

        [HttpDelete("{projectPhaseId}")]
        public async Task<IActionResult> Delete(Guid projectPhaseId)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { ProjectPhaseId = projectPhaseId }));
        }  
    }
}