// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;
using AppCore.CommandModel.Pipeline;
using AppCore.CommandModel.Validation;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to register validation with the <see cref="CommandModelFacility"/>.
    /// </summary>
    public static class CommandModelFacilityExtensions
    {
        /// <summary>
        /// Registers command validation pipeline behavior.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument <paramref name="facility"/> is <c>null</c>.</exception>
        public static CommandModelFacility UseValidation(this CommandModelFacility facility)
        {
            Ensure.Arg.NotNull(facility, nameof(facility));

            facility.ConfigureRegistry(
                r =>
                {
                    r.AddModelValidation();

                    r.TryAddEnumerable(
                        ComponentRegistration.Create(
                            typeof(ICommandPipelineBehavior<,>),
                            typeof(CommandValidationBehavior<,>),
                            facility.Lifetime));
                });

            return facility;
        }
    }
}