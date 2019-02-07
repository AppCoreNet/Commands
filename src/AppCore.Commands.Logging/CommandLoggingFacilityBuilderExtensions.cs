// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using AppCore.DependencyInjection.Facilities;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    public static class CommandLoggingFacilityBuilderExtensions
    {
        /// <summary>
        /// Registers command logging pipeline behavior.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityBuilder<ICommandsFacility> AddLogging(this IFacilityBuilder<ICommandsFacility> builder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));
            return builder.AddExtension<CommandLoggingFacilityExtension>();
        }
    }
}