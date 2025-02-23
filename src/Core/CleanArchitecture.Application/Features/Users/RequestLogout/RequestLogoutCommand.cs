using CleanArchitecture.Application.Models.Common;
using Mediator;

namespace CleanArchitecture.Application.Features.Users.RequestLogout;

public record RequestLogoutCommand(int UserId):IRequest<OperationResult<bool>>;