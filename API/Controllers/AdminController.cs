﻿using Application.AppUsers.DTOs;
using Application.Developers.DTOs;
using Application.ProductManagers.DTOs;
using Application.ProjectManagers.DTOs;
using Domain.ModelsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : BaseApiController
    {
        [HttpPost("users/developer")]
        public async Task<IActionResult> Create(DeveloperRegisterRequest developerDto)
        {
            return HandleResult(await Mediator.Send(new Application.Developers.Create.Command { Developer = developerDto }));
        }

        [HttpPost("users/product-manager")]
        public async Task<IActionResult> Create(ProductManagerRegisterRequest productManagerDto)
        {
            return HandleResult(await Mediator.Send(new Application.ProductManagers.Create.Command { ProductManager = productManagerDto }));
        }

        [HttpPost("users/project-manager")]
        public async Task<IActionResult> Create(ProjectManagerRegisterRequest projectManagerDto)
        {
            return HandleResult(await Mediator.Send(new Application.ProjectManagers.Create.Command { ProjectManager = projectManagerDto }));
        }

        [HttpDelete("users/{appUserId}")]
        public async Task<IActionResult> Delete(string appUserId)
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.Delete.Command { AppUserId = appUserId }));
        }

        [HttpGet("search/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.GetAllUsers.Query()));
        }

        [HttpGet("users/admin/{adminId}")]
        public async Task<IActionResult> GetAdminById(string adminId)
        {
            return HandleResult(await Mediator.Send(new Application.Admins.Details.Query { AppUserId = adminId}));
        }

        [HttpGet("search/{search}")]
        public async Task<IActionResult> SearchUsers(string search)
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.GetBySearchQuery.Query { SearchQuery = search }));
        }

        [HttpPatch("users/{appUserId}")]
        public async Task<IActionResult> UpdateUser(string appUserId, UserPatchRequest userUpdateRequest)
        {
            return HandleResult(await Mediator.Send(new Application.AppUsers.Patch.Command { UserId = appUserId, UserPatch = userUpdateRequest }));
        }
    }
}
