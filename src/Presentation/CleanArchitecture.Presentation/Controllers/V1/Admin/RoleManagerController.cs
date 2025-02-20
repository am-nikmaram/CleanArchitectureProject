using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using CleanArchitecture.Application.Features.Role.AddRoleCommand;
using CleanArchitecture.Application.Features.Role.GetAllRolesQuery;
using CleanArchitecture.Application.Features.Role.GetAuthorizableRoutesQuery;
using CleanArchitecture.Application.Features.Role.UpdateRoleClaimsCommand;
using CleanArchitecture.Infrastructure.Identity.Identity.PermissionManager;
using CleanArchitecture.WebFramework.Attributes;
using CleanArchitecture.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers.V1.Admin;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/RoleManager")]
[Authorize(ConstantPolicies.DynamicPermission)]
[Display(Description = "Managing Related Roles for the System")]

public class RoleManagerController(ISender sender) : BaseController
{
    [HttpGet("Roles")]
    [ProducesOkApiResponseType<List<GetAllRolesQueryResponse>>]
    public async Task<IActionResult> GetRoles()
    {
        var queryResult = await sender.Send(new GetAllRolesQuery());

        return base.OperationResult(queryResult);
    }

    [HttpGet("AuthRoutes")]
    [ProducesOkApiResponseType<List<GetAuthorizableRoutesQueryResponse>>]
    public async Task<IActionResult> GetAuthRoutes()
    {
        var queryModel = await sender.Send(new GetAuthorizableRoutesQuery());

        return base.OperationResult(queryModel);
    }

    /// <summary>
    /// Update a role permissions (claims) based on RouteKey received in AuthRoutes API
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("UpdateRolePermissions")]
    [ProducesOkApiResponseType]
    public async Task<IActionResult> UpdateRolePermissions(UpdateRoleClaimsCommand model)
    {
        var commandResult =
            await sender.Send(new UpdateRoleClaimsCommand(model.RoleId, model.RoleClaimValue));

        return base.OperationResult(commandResult);
    }

    [HttpPost("NewRole")]
    [ProducesOkApiResponseType]
    public async Task<IActionResult> AddRole(AddRoleCommand model)
    {
        var commandResult = await sender.Send(model);

        return base.OperationResult(commandResult);
    }

}
