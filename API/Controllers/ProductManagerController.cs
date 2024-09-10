using Application.ProductManagers;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductManagerController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(ProductManagerDto productManagerDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { ProductManager = productManagerDto }));
        }

        [HttpGet("{appUserId}")]
        public async Task<IActionResult> Get(string appUserId)
        {
            return HandleResult(await Mediator.Send(new Details.Query { AppUserId = appUserId }));
        }
    }
}