using Asp.Versioning;
using CleanArchitecture.Application.Features.Admin.AddAdminCommand;
using CleanArchitecture.Application.Features.Admin.GetToken;
using CleanArchitecture.Application.Models.Jwt;
using CleanArchitecture.WebFramework.Attributes;
using CleanArchitecture.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers.V1.Admin;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/AdminManager")]
public class AdminManagerController(ISender sender) : BaseController
{
    [HttpPost("Login")]
    [ProducesOkApiResponseType<AccessToken>]
    public async Task<IActionResult> AdminLogin(AdminGetTokenQuery model)
    {
        var query = await sender.Send(model);

        return base.OperationResult(query);
    }

    [Authorize(Roles = "admin")]
    [HttpPost("NewAdmin")]
    [ProducesOkApiResponseType]
    public async Task<IActionResult> AddNewAdmin(AddAdminCommand model)
    {
        var commandResult = await sender.Send(model);

        return base.OperationResult(commandResult);
    }
}
