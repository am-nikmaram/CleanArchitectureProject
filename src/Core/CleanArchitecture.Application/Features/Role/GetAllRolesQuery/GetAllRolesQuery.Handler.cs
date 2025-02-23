using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Common;
using Mediator;

namespace CleanArchitecture.Application.Features.Role.GetAllRolesQuery;

internal class GetAllRolesQueryHandler(IRoleManagerService roleManagerService)
    : IRequestHandler<GetAllRolesQuery, OperationResult<List<GetAllRolesQueryResponse>>>
{
    public async ValueTask<OperationResult<List<GetAllRolesQueryResponse>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleManagerService.GetRolesAsync();

        if(!roles.Any())
            return OperationResult<List<GetAllRolesQueryResponse>>.NotFoundResult("No Roles Found");

        var result = roles.Select(c => new GetAllRolesQueryResponse(int.Parse(c.Id), c.Name)).ToList();

        return OperationResult<List<GetAllRolesQueryResponse>>.SuccessResult(result);
    }
}
