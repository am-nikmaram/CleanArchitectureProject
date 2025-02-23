using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Shared.ValidationBase;
using CleanArchitecture.Shared.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArchitecture.Application.Features.Admin.AddAdminCommand;

public record AddAdminCommand
    (string UserName, string Email, string Password, int RoleId) : IRequest<OperationResult<bool>>,
        IValidatableModel<AddAdminCommand>
{
    public IValidator<AddAdminCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AddAdminCommand> validator)
    {
        validator.RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("Please enter an valid email");

        validator.RuleFor(c => c.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please specify a valid username");

        validator
            .RuleFor(c => c.RoleId)
            .GreaterThan(0)
            .WithMessage("Please select a valid role");

        return validator;
    }
};