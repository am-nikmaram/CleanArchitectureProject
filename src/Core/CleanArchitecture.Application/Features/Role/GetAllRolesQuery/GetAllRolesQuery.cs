using CleanArchitecture.Application.Models.Common;
using Mediator;

namespace CleanArchitecture.Application.Features.Role.GetAllRolesQuery;

public record GetAllRolesQuery():IRequest<OperationResult<List<GetAllRolesQueryResponse>>>;