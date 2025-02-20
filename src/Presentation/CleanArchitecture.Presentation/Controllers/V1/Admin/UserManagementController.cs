using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using Mediator;
using CleanArchitecture.Infrastructure.Identity.Identity.PermissionManager;
using CleanArchitecture.WebFramework.BaseController;
using CleanArchitecture.Application.Features.Users.GetUsers;
using CleanArchitecture.WebFramework.Attributes;

namespace CleanArchitecture.Presentation.Controllers.V1.Admin;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/UserManagement")]
[Display(Description = "Managing API Users")]
[Authorize(ConstantPolicies.DynamicPermission)]
public class UserManagementController : BaseController
{
    private readonly ISender _sender;

    public UserManagementController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("CurrentUsers")]
    [ProducesOkApiResponseType<List<GetUsersQueryResponse>>]
    public async Task<IActionResult> GetAllUsers()
    {
        var queryResult = await _sender.Send(new GetUsersQuery());

        return base.OperationResult(queryResult);
    }
}
