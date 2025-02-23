using Asp.Versioning;
using CleanArchitecture.Application.Features.Users.Create;
using CleanArchitecture.Application.Features.Users.GenerateUserToken;
using CleanArchitecture.Application.Features.Users.RefreshUserTokenCommand;
using CleanArchitecture.Application.Features.Users.RequestLogout;
using CleanArchitecture.Application.Features.Users.TokenRequest;
using CleanArchitecture.Application.Models.Jwt;
using CleanArchitecture.WebFramework.Attributes;
using CleanArchitecture.WebFramework.BaseController;
using CleanArchitectureWebFramework.Swagger;
using Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Web.Api.Controllers.V1.UserManagement;

[ApiVersion("1")]
[ApiController]
[Route("api/v{version:apiVersion}/User")]
public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    [ProducesOkApiResponseType<UserCreateCommandResult>]
    public async Task<IActionResult> CreateUser(UserCreateCommand model)
    {
        var command = await _mediator.Send(model);

        return base.OperationResult(command);
    }


    [HttpPost("TokenRequest")]
    [ProducesOkApiResponseType<UserTokenRequestQueryResponse>]
    public async Task<IActionResult> TokenRequest(UserTokenRequestQuery model)
    {
        var query = await _mediator.Send(model);

        return base.OperationResult(query);
    }

    [HttpPost("LoginConfirmation")]
    [ProducesOkApiResponseType<AccessToken>]
    public async Task<IActionResult> ValidateUser(GenerateUserTokenQuery model)
    {
        var result = await _mediator.Send(model);

        return base.OperationResult(result);
    }

    [HttpPost("RefreshSignIn")]
    [RequireTokenWithoutAuthorization]
    [ProducesOkApiResponseType<AccessToken>]
    public async Task<IActionResult> RefreshUserToken(RefreshUserTokenCommand model)
    {
        var checkCurrentAccessTokenValidity =await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

        if (checkCurrentAccessTokenValidity.Succeeded)
            return BadRequest("Current access token is valid. No need to refresh");

        var newTokenResult = await _mediator.Send(model);

        return base.OperationResult(newTokenResult);
    }

    [HttpPost("Logout")]
    [Authorize]
    [ProducesOkApiResponseType]
    public async Task<IActionResult> RequestLogout()
    {
        var commandResult = await _mediator.Send(new RequestLogoutCommand(base.UserId));

        return base.OperationResult(commandResult);
    }
}