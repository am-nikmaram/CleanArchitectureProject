using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Common;
using Mediator;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Users.TokenRequest;

public class UserTokenRequestQueryHandler(
    IAppUserManager userManager,
    ILogger<UserTokenRequestQueryHandler> logger)
    : IRequestHandler<UserTokenRequestQuery, OperationResult<UserTokenRequestQueryResponse>>
{
  
    
    public async ValueTask<OperationResult<UserTokenRequestQueryResponse>> Handle(UserTokenRequestQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserByPhoneNumber(request.UserPhoneNumber);

        if(user is null)
            return OperationResult<UserTokenRequestQueryResponse>.NotFoundResult("User Not found");

        var code = user.PhoneNumberConfirmed? await userManager.GenerateOtpCode(user) : await userManager.GeneratePhoneNumberConfirmationToken(user,user.PhoneNumber);

        logger.LogWarning($"Generated Code for user Id {user.Id} is {code}");

        //TODO Send Code Via Sms Provider

        return OperationResult<UserTokenRequestQueryResponse>.SuccessResult(new UserTokenRequestQueryResponse {UserKey = user.GeneratedCode});
    }
}