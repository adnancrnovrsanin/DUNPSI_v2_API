using Application.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DashboardController : BaseApiController
    {
        [HttpGet("manager/{managerId}")]
        public async Task<IActionResult> GetManagerDashboard(Guid managerId)
        {
            return HandleResult(await Mediator.Send(new GetManagerDashboard.Query { ManagerId = managerId }));
        }

        [HttpGet("company")]
        public async Task<IActionResult> GetCompanyDashboard()
        {
            return HandleResult(await Mediator.Send(new GetCompanyDashboard.Query()));
        }
    }
}
