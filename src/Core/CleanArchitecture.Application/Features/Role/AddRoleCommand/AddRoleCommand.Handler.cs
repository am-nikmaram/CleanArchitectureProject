using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Application.Models.Identity;
using Mediator;

namespace CleanArchitecture.Application.Features.Role.AddRoleCommand;

internal class AddRoleCommandHandler(IRoleManagerService roleManagerService)
    : IRequestHandler<AddRoleCommand, OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var addRoleResult =
            await roleManagerService.CreateRoleAsync(new CreateRoleDto() { RoleName = request.RoleName });

        if (addRoleResult.Succeeded)
            return OperationResult<bool>.SuccessResult(true);

        var errors = string.Join("\n", addRoleResult.Errors.Select(c => c.Description));

        return OperationResult<bool>.FailureResult(errors);
    }
}
