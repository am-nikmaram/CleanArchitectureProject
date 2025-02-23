using CleanArchitecture.Application.Models.Common;
using Mediator;

namespace CleanArchitecture.Application.Features.Role.UpdateRoleClaimsCommand;

public record UpdateRoleClaimsCommand( int RoleId, List<string> RoleClaimValue):IRequest<OperationResult<bool>>;