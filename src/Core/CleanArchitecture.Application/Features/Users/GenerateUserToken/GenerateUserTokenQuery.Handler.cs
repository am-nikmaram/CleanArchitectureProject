﻿using CleanArc.SharedKernel.Extensions;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Application.Models.Jwt;
using Mediator;

namespace CleanArchitecture.Application.Features.Users.GenerateUserToken;

internal class GenerateUserTokenQueryHandler(IJwtService jwtService, IAppUserManager userManager)
    : IRequestHandler<GenerateUserTokenQuery, OperationResult<AccessToken>>
{
    public async ValueTask<OperationResult<AccessToken>> Handle(GenerateUserTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserByCode(request.UserKey);

        if (user is null)
            return OperationResult<AccessToken>.FailureResult("User Not found");

        var result = user.PhoneNumberConfirmed? await userManager.VerifyUserCode(
            user, request.Code):await userManager.ChangePhoneNumber(user,user.PhoneNumber,request.Code);


        if (!result.Succeeded)
            return OperationResult<AccessToken>.FailureResult(result.Errors.StringifyIdentityResultErrors());

        await userManager.UpdateUserAsync(user);

        var token = await jwtService.GenerateAsync(user);

        return OperationResult<AccessToken>.SuccessResult(token);
    }
}