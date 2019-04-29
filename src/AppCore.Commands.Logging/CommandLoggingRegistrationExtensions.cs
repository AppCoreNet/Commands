// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using AppCore.Commands;
using AppCore.Commands.Logging;
using AppCore.DependencyInjection.Facilities;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to register logging with the <see cref="ICommandsFacility"/>.
    /// </summary>
    public static class CommandLoggingRegistrationExtensions
    {
        /// <summary>
        /// Registers command logging pipeline behavior.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityExtensionBuilder<ICommandsFacility, CommandLoggingExtension> WithLogging(
            this IFacilityBuilder<ICommandsFacility> builder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));
            return builder.AddExtension<CommandLoggingExtension>();
        }
    }
}