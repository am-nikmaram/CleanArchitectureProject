﻿using CleanArchitecture.Application.Models.Common;
using CleanArchitecture.Application.Models.Jwt;
using CleanArchitecture.Shared.ValidationBase;
using CleanArchitecture.Shared.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArchitecture.Application.Features.Admin.GetToken;

public record AdminGetTokenQuery(string UserName, string Password) : IRequest<OperationResult<AccessToken>>,
    IValidatableModel<AdminGetTokenQuery>
{
    public IValidator<AdminGetTokenQuery> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AdminGetTokenQuery> validator)
    {
        validator.RuleFor(c => c.UserName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter admin username");

        validator.RuleFor(c => c.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter admin password");

        return validator;
    }
};