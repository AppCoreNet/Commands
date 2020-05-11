// Licensed under the MIT License.
// Copyright (c) 2020 the AppCore .NET project.

using AppCore.Commands.Metadata;
using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection;
using AppCore.DependencyInjection.Facilities;

namespace AppCore.Commands
{
    /// <summary>
    /// Provides authentication extensions for the <see cref="ICommandsFacility"/>.
    /// </summary>
    public class CommandAuthenticationExtension : FacilityExtension<ICommandsFacility>
    {
        /// <inheritdoc />
        protected override void RegisterComponents(IComponentRegistry registry, ICommandsFacility facility)
        {
            registry.Register<ICommandPrincipalProvider>()
                    .Add<CommandPrincipalProvider>()
                    .PerContainer()
                    .IfNotRegistered();

            registry.Register<ICommandMetadataProvider>()
                    .Add<AuthorizedCommandMetadataProvider>()
                    .PerContainer()
                    .IfNotRegistered();

            registry.Register(typeof(ICommandPipelineBehavior<,>))
                    .Add(typeof(AuthenticatedCommandBehavior<,>))
                    .WithLifetime(facility.Lifetime)
                    .IfNotRegistered();
        }
    }
}
