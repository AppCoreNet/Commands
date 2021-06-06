// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;
using AppCore.CommandModel.Logging;
using AppCore.CommandModel.Pipeline;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to register logging with the <see cref="CommandModelFacility"/>.
    /// </summary>
    public static class CommandModelFacilityExtensions
    {
        /// <summary>
        /// Adds logging of events to the pipeline.
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument <paramref name="facility"/> is <c>null</c>.</exception>
        public static CommandModelFacility UseLogging(this CommandModelFacility facility)
        {
            Ensure.Arg.NotNull(facility, nameof(facility));

            facility.ConfigureRegistry(
                r =>
                {
                    r.AddLogging();

                    r.TryAddEnumerable(
                        ComponentRegistration.Create(
                            typeof(ICommandPipelineBehavior<,>),
                            typeof(CommandLoggingBehavior<,>)));
                });

            return facility;
        }
    }
}