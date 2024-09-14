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
        public async Task<IActionResult> RateDeveloper(RatingDto ratingDto)
        {
            return HandleResult(await Mediator.Send(new RateDeveloper.Command { RatingDto = ratingDto }));
        }
    }
}