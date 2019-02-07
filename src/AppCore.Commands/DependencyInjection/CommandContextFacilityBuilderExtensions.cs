﻿// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using AppCore.Commands.Pipeline;
using AppCore.Diagnostics;
using AppCore.DependencyInjection.Facilities;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extensions methods to configure the <see cref="ICommandsFacility"/>.
    /// </summary>
    public static class CommandContextFacilityBuilderExtensions
    {
        /// <summary>
        /// Registers the <see cref="ICommandContextAccessor"/> with the DI container.
        /// </summary>
        /// <param name="builder">The <see cref="IFacilityBuilder{TFacility}"/>.</param>
        /// <returns>The passed <paramref name="builder"/>.</returns>
        public static IFacilityBuilder<ICommandsFacility> AddCommandContextAccessor(
            this IFacilityBuilder<ICommandsFacility> builder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));
            return builder.AddExtension<CommandContextFacilityExtension>();
        }
    }
}