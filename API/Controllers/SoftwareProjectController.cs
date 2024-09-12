using Application.SoftwareProjects;
using Application.SoftwareProjects.DTOs;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class SoftwareProjectController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDto projectCreateDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { SoftwareProject = projectCreateDto }));
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProject(Guid projectId)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = projectId }));
        }

        [HttpGet("manager-requests/{managerId}")]
        public async Task<IActionResult> GetManagerRequests(Guid managerId)
        {
            return HandleResult(await Mediator.Send(new ListManagerRequests.Query { ProjectManagerId = managerId }));
        }

        [HttpPut("manager-requests/reject")]
        public async Task<IActionResult> RejectManagerRequest(InitialProjectRequestDto rejectManagerRequestDto)
        {
            return HandleResult(await Mediator.Send(new RejectManagerRequest.Command { ProjectRequest = rejectManagerRequestDto }));
        }

        [HttpPost("request-manager")]
        public async Task<IActionResult> RequestManager(InitialProjectRequestDto requestManagerDto)
        {
            return HandleResult(await Mediator.Send(new RequestManager.Command { ProjectRequest = requestManagerDto }));
        }

        [HttpPost("initial-request")]
        public async Task<IActionResult> InitialRequest(InitialProjectRequestDto initialProjectRequestDto)
        {
            return HandleResult(await Mediator.Send(new InitialRequest.Command { InitialProjectRequest = initialProjectRequestDto }));
        }

        [HttpPut("reject-request/{projectRequestId}")]
        public async Task<IActionResult> RejectRequest(Guid projectRequestId)
        {
            return HandleResult(await Mediator.Send(new RejectRequest.Command { ProjectRequestId = projectRequestId }));
        }

        [HttpGet("project-requests")]
        public async Task<IActionResult> GetProjectRequests()
        {
            return HandleResult(await Mediator.Send(new ListProjectRequests.Query()));
        }

        [HttpGet("project-requests/{projectRequestId}")]
        public async Task<IActionResult> GetProjectRequest(Guid projectRequestId)
        {
            return HandleResult(await Mediator.Send(new GetProjectRequest.Query { ProjectRequestId = projectRequestId }));
        }

        [HttpGet("project-phases/{projectId}")]
        public async Task<IActionResult> GetProjectPhases(Guid projectId)
        {
            return HandleResult(await Mediator.Send(new ListProjectPhases.Query { ProjectId = projectId }));
        }

        [HttpPut("project-phases/requirement-layout")]
        public async Task<IActionResult> UpdateRequirementLayout(UpdateRequirementLayoutDto updateRequirementLayoutDto)
        {
            return HandleResult(await Mediator.Send(new UpdateRequirementLayout.Command { UpdateRequest = updateRequirementLayoutDto }));
        }

        [HttpPut("finish-project/{projectId}")]
        public async Task<IActionResult> FinishProject(Guid projectId)
        {
            return HandleResult(await Mediator.Send(new FinishProject.Command { ProjectId = projectId }));
        }

        [HttpPost("requirements/{projectId}")]
        public async Task<IActionResult> GetRequirementsForApproval(RequirementsRequestParams requirementsRequestParams)
        {
            return HandleResult(await Mediator.Send(new GetRequirementsForApproval.Query { Params = requirementsRequestParams }));
        }
    }
}