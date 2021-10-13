// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to register authentication with the <see cref="CommandModelFacility"/>.
    /// </summary>
    public static class CommandModelFacilityExtensions
    {
        /// <summary>
        /// Registers command authentication pipeline behavior.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument <paramref name="facility"/> is <c>null</c>.</exception>
        public static CommandModelFacility UseAuthentication(this CommandModelFacility facility)
        {
            Ensure.Arg.NotNull(facility, nameof(facility));
            facility.AddExtension<AuthenticatedCommandFacilityExtension>();
            return facility;
        }
    }
}