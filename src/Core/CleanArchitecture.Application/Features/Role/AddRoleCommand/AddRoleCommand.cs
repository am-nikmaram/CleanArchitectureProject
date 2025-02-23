using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Shared.ValidationBase;
using CleanArchitecture.Shared.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArchitecture.Application.Features.Role.AddRoleCommand;

public record AddRoleCommand(string RoleName) : IRequest<OperationResult<bool>>,
    IValidatableModel<AddRoleCommand>
{
    public IValidator<AddRoleCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AddRoleCommand> validator)
    {
        validator
            .RuleFor(c => c.RoleName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter role name");

        return validator;
    }
};