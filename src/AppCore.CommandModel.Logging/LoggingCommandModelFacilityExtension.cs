// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using AppCore.CommandModel.Logging;
using AppCore.CommandModel.Pipeline;
using AppCore.DependencyInjection.Facilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides a extension for <see cref="CommandModelFacility"/> to add support for logging.
    /// </summary>
    public class LoggingCommandModelFacilityExtension : FacilityExtension
    {
        /// <inheritdoc />
        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.TryAddEnumerable(
                ServiceDescriptor.Singleton(
                    typeof(ICommandPipelineBehavior<,>),
                    typeof(CommandLoggingBehavior<,>)));
        }
    }
}