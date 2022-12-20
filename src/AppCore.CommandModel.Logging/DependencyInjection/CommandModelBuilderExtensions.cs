// Licensed under the MIT License.
// Copyright (c) 2018-2022 the AppCore .NET project.

using System;
using AppCore.CommandModel.Logging;
using AppCore.CommandModel.Pipeline;
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
    /// Adds logging of events to the pipeline.
    /// </summary>
    /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
    public static ICommandModelBuilder AddLogging(this ICommandModelBuilder builder)
    {
        Ensure.Arg.NotNull(builder);

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton(
                typeof(ICommandPipelineBehavior<,>),
                typeof(CommandLoggingBehavior<,>)));

        return builder;
    }
}