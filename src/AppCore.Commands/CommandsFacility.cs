// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using AppCore.Commands;
using AppCore.Commands.Metadata;
using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection.Facilities;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides the default <see cref="ICommandsFacility"/> implementation.
    /// </summary>
    public class CommandsFacility : Facility, ICommandsFacility
    {
        /// <summary>
        /// Gets or sets the lifetime when registering components.
        /// </summary>
        public ComponentLifetime Lifetime { get; set; } = ComponentLifetime.Scoped;

        /// <inheritdoc />
        protected override void RegisterComponents(IComponentRegistry registry)
        {
            registry.Register<ICommandProcessor>()
                    .Add<CommandProcessor>()
                    .WithLifetime(Lifetime)
                    .IfNoneRegistered();

            registry.Register<ICommandDescriptorFactory>()
                    .Add<CommandDescriptorFactory>()
                    .PerContainer()
                    .IfNoneRegistered();

            registry.Register<ICommandMetadataProvider>()
                    .Add<CancelableCommandMetadataProvider>()
                    .PerContainer()
                    .IfNotRegistered();

            registry.Register(typeof(ICommandPipelineBehavior<,>))
                    .Add(typeof(CancelableCommandBehavior<,>))
                    .PerContainer()
                    .IfNotRegistered();

            registry.Register(typeof(ICommandPipelineBehavior<,>))
                    .Add(typeof(PreCommandHandlerBehavior<,>))
                    .WithLifetime(Lifetime)
                    .IfNotRegistered();

            registry.Register(typeof(ICommandPipelineBehavior<,>))
                    .Add(typeof(PostCommandHandlerBehavior<,>))
                    .WithLifetime(Lifetime)
                    .IfNotRegistered();
        }
    }
}
