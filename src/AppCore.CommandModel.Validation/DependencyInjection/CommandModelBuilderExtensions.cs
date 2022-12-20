// Licensed under the MIT License.
// Copyright (c) 2018-2022 the AppCore .NET project.

using System;
using AppCore.CommandModel.Pipeline;
using AppCore.CommandModel.Validation;
using AppCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace AppCore.Extensions.DependencyInjection;

/// <summary>
/// Provides extensions to register the command model.
/// </summary>
public static class CommandModelBuilderExtensions
{
    /// <summary>
    /// Registers command validation pipeline behavior.
    /// </summary>
    /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
    public static ICommandModelBuilder AddValidation(this ICommandModelBuilder builder)
    {
        Ensure.Arg.NotNull(builder);

        IServiceCollection services = builder.Services;

        services.AddAppCore()
               .AddModelValidation();

        services.TryAddEnumerable(
            ServiceDescriptor.Transient(
                typeof(ICommandPipelineBehavior<,>),
                typeof(CommandValidationBehavior<,>)));

        return builder;
    }
}