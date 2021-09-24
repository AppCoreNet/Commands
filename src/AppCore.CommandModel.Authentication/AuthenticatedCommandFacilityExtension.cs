// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using AppCore.CommandModel.Metadata;
using AppCore.CommandModel.Pipeline;
using AppCore.DependencyInjection.Facilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides a extension for <see cref="CommandModelFacility"/> to add support for authenticated commands.
    /// </summary>
    public class AuthenticatedCommandFacilityExtension : FacilityExtension
    {
        /// <inheritdoc />
        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.TryAddEnumerable(
                new[]
                {
                    ServiceDescriptor.Singleton<ICommandPrincipalProvider, CommandPrincipalProvider>(),
                    ServiceDescriptor.Singleton<ICommandMetadataProvider, AuthorizedCommandMetadataProvider>(),
                    ServiceDescriptor.Describe(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(AuthenticatedCommandBehavior<,>),
                        ((CommandModelFacility)Facility).Lifetime)
                });
        }
    }
}