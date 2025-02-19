using CleanArchitecture.Application.Models.Common;
using Mediator;

namespace CleanArchitecture.Application.Features.Role.GetAuthorizableRoutesQuery;

public record GetAuthorizableRoutesQuery():IRequest<OperationResult<List<GetAuthorizableRoutesQueryResponse>>>;