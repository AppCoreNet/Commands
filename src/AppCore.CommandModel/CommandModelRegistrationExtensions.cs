// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to configure the <see cref="CommandModelFacility"/>.
    /// </summary>
    public static class CommandModelRegistrationExtensions
    {
        /// <summary>
        /// Adds the <see cref="CommandModelFacility"/> to the DI container.
        /// </summary>
        /// <param name="registry">The <see cref="IComponentRegistry"/>.</param>
        /// <param name="configure">The configure delegate.</param>
        /// <returns></returns>
        public static IComponentRegistry AddCommandModel(
            this IComponentRegistry registry,
            Action<CommandModelFacility> configure = null)
        {
            return registry.AddFacility(configure);
        }
    }
}