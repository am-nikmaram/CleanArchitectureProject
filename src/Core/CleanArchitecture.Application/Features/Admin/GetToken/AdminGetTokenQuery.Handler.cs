﻿using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Application.Models.Jwt;
using Mediator;

namespace CleanArchitecture.Application.Features.Admin.GetToken;

public class AdminGetTokenQueryHandler:IRequestHandler<AdminGetTokenQuery,OperationResult<AccessToken>>
{
    private readonly IAppUserManager _userManager;
    private readonly IJwtService _jwtService;
    public AdminGetTokenQueryHandler(IAppUserManager userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async ValueTask<OperationResult<AccessToken>> Handle(AdminGetTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByUserName(request.UserName);

        if(user is null)
            return OperationResult<AccessToken>.FailureResult("User not found");

        var isUserLockedOut = await _userManager.IsUserLockedOutAsync(user);

        if(isUserLockedOut)
            if (user.LockoutEnd != null)
                return OperationResult<AccessToken>.FailureResult(
                    $"User is locked out. Try in {(user.LockoutEnd-DateTimeOffset.Now).Value.Minutes} Minutes");

        var passwordValidator = await _userManager.AdminLogin(user, request.Password);


        if (!passwordValidator.Succeeded)
        {
          var lockoutIncrementResult= await _userManager.IncrementAccessFailedCountAsync(user);

            return OperationResult<AccessToken>.FailureResult("Password is not correct");
        }

        var token= await _jwtService.GenerateAsync(user);


        return OperationResult<AccessToken>.SuccessResult(token);
    }
}