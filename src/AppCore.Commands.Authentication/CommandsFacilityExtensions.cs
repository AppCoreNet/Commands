// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to register authentication with the <see cref="CommandsFacility"/>.
    /// </summary>
    public static class CommandsFacilityExtensions
    {
        /// <summary>
        /// Registers command authentication pipeline behavior.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static CommandsFacility UseAuthentication(this CommandsFacility facility)
        {
            Ensure.Arg.NotNull(facility, nameof(facility));

            facility.ConfigureRegistry(
                r =>
                {
                    r.TryAddEnumerable(
                        new[]
                        {
                            ComponentRegistration.Singleton<ICommandPrincipalProvider, CommandPrincipalProvider>(),
                            ComponentRegistration
                                .Singleton<ICommandMetadataProvider, AuthorizedCommandMetadataProvider>(),
                            ComponentRegistration.Create(
                                typeof(ICommandPipelineBehavior<,>),
                                typeof(AuthenticatedCommandBehavior<,>),
                                facility.Lifetime)
                        });
                });

            return facility;
        }
    }
}