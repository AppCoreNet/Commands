// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using AppCore.DependencyInjection.Facilities;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to register validation with the <see cref="ICommandsFacility"/>.
    /// </summary>
    public static class CommandValidationFacilityBuilderExtensions
    {
        /// <summary>
        /// Registers command validation pipeline behavior.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityBuilder<ICommandsFacility> AddValidation(this IFacilityBuilder<ICommandsFacility> builder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));
            return builder.AddExtension<CommandValidationFacilityExtension>();
        }
    }
}