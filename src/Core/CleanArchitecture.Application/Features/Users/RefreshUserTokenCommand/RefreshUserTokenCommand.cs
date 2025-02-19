using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Application.Models.Jwt;
using CleanArchitecture.Shared.ValidationBase;
using CleanArchitecture.Shared.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArchitecture.Application.Features.Users.RefreshUserTokenCommand;

public record RefreshUserTokenCommand(Guid RefreshToken) : IRequest<OperationResult<AccessToken>>,
    IValidatableModel<RefreshUserTokenCommand>
{
    public IValidator<RefreshUserTokenCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<RefreshUserTokenCommand> validator)
    {
        validator.RuleFor(c => c.RefreshToken)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter valid user refresh token");

        return validator;
    }
};