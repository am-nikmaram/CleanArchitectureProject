﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Shared.ValidationBase;

public class ApplicationBaseValidationModelProvider<TApplicationModel>:AbstractValidator<TApplicationModel>;