using AutoMapper;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Domain.Entities.User;
using Mediator;

namespace CleanArchitecture.Application.Features.Users.GetUsers;

internal class GetUsersQueryHandler(IAppUserManager userManager, IMapper mapper)
    : IRequestHandler<GetUsersQuery, OperationResult<List<GetUsersQueryResponse>>>
{
    public async ValueTask<OperationResult<List<GetUsersQueryResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var usersModel =
            (await userManager.GetAllUsersAsync()).Select(mapper.Map<User, GetUsersQueryResponse>).ToList();

        if(!usersModel.Any())
            return OperationResult<List<GetUsersQueryResponse>>.NotFoundResult("No Users Found!");

        return OperationResult<List<GetUsersQueryResponse>>.SuccessResult(usersModel);
    }
}