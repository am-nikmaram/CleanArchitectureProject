using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Application.Models.Jwt;
using CleanArchitecture.Shared.ValidationBase;
using CleanArchitecture.Shared.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArchitecture.Application.Features.Users.GenerateUserToken;

public record GenerateUserTokenQuery(string UserKey, string Code) : IRequest<OperationResult<AccessToken>>,
    IValidatableModel<GenerateUserTokenQuery>
{
    public IValidator<GenerateUserTokenQuery> ValidateApplicationModel(ApplicationBaseValidationModelProvider<GenerateUserTokenQuery> validator)
    {
        validator.RuleFor(c => c.Code)
            .NotEmpty()
            .NotNull()
            .Length(6)
            .WithMessage("User code is not valid");

        validator.RuleFor(c => c.UserKey)
            .NotEmpty()
            .NotNull()
            .WithMessage("Invalid user key");

        return validator;
    }
};