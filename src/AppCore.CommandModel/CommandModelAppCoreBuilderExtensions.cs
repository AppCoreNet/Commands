// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;
using AppCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to configure the <see cref="CommandModelFacility"/>.
    /// </summary>
    public static class CommandModelAppCoreBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="CommandModelFacility"/> to the DI container.
        /// </summary>
        /// <param name="builder">The <see cref="IAppCoreBuilder"/>.</param>
        /// <param name="configure">The configure delegate.</param>
        /// <returns>The passed <see cref="IAppCoreBuilder"/>.</returns>
        public static IAppCoreBuilder AddCommandModel(
            this IAppCoreBuilder builder,
            Action<CommandModelFacility> configure = null)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));
            builder.Services.AddFacility(configure);
            return builder;
        }
    }
}