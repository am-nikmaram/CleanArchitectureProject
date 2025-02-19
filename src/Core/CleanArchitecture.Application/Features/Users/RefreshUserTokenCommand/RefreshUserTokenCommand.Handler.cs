using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Application.Models.Jwt;
using Mediator;

namespace CleanArchitecture.Application.Features.Users.RefreshUserTokenCommand;

internal class RefreshUserTokenCommandHandler(IJwtService jwtService)
    : IRequestHandler<RefreshUserTokenCommand, OperationResult<AccessToken>>
{
    public async ValueTask<OperationResult<AccessToken>> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
    {
        var newToken = await jwtService.RefreshTokenAsync(request.RefreshToken);

        if(newToken is null)
            return OperationResult<AccessToken>.FailureResult("Invalid refresh token");

        return OperationResult<AccessToken>.SuccessResult(newToken);
    }
}
