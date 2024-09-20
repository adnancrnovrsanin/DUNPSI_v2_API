using Application.Ratings;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class RatingController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> RateRequirementQuality(RatingDto ratingDto)
        {
            return HandleResult(await Mediator.Send(new RateRequirementQuality.Command { RatingDto = ratingDto }));
        }
    }
}