// Licensed under the MIT License.
// Copyright (c) 2020 the AppCore .NET project.

using System;
using AppCore.Commands;
using AppCore.DependencyInjection.Facilities;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to register authentication with the <see cref="ICommandsFacility"/>.
    /// </summary>
    public static class CommandAuthenticationRegistrationExtensions
    {
        /// <summary>
        /// Registers command authentication pipeline behavior.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityBuilder<ICommandsFacility> AddAuthentication(
            this IFacilityBuilder<ICommandsFacility> builder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));
            return builder.Add<CommandAuthenticationExtension>();
        }
    }
}