using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Application.Models.Identity;
using Mediator;

namespace CleanArchitecture.Application.Features.Role.UpdateRoleClaimsCommand;

internal class UpdateRoleClaimsCommandHandler(IRoleManagerService roleManagerService)
    : IRequestHandler<UpdateRoleClaimsCommand, OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(UpdateRoleClaimsCommand request, CancellationToken cancellationToken)
    {
        var updateRoleResult = await roleManagerService.ChangeRolePermissionsAsync(new EditRolePermissionsDto()
            { RoleId = request.RoleId, Permissions = request.RoleClaimValue });

        return updateRoleResult
            ? OperationResult<bool>.SuccessResult(true)
            : OperationResult<bool>.FailureResult("Could Not Update Claims for given Role");
    }
}
