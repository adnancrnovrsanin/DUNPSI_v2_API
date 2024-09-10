using Application.Photos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotosController : BaseApiController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("{userId}")]
        public async Task<IActionResult> Add(string userId, [FromForm] IFormFile file)
        {
            return HandleResult(await Mediator.Send(new Add.Command { File = file, UserId = userId }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPut("{userId}/setmain/{id}")]
        public async Task<IActionResult> SetMain(string userId, string id)
        {
            return HandleResult(await Mediator.Send(new SetMain.Command { UserId = userId, Id = id }));
        }
    }
}