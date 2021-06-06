// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;
using AppCore.CommandModel;
using AppCore.CommandModel.Metadata;
using AppCore.CommandModel.Pipeline;
using AppCore.DependencyInjection.Facilities;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides the commands facility.
    /// </summary>
    public class CommandModelFacility : Facility
    {
        /// <summary>
        /// Gets the lifetime of the command pipeline components.
        /// </summary>
        public ComponentLifetime Lifetime { get; private set; } = ComponentLifetime.Scoped;

        /// <summary>
        /// Registers the <see cref="ICommandContextAccessor"/> with the DI container.
        /// </summary>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        public CommandModelFacility WithEventContext()
        {
            ConfigureRegistry(
                r => r.TryAdd(
                    ComponentRegistration.Singleton<ICommandContextAccessor, CommandContextAccessor>()
                )
            );

            return this;
        }

        /// <summary>
        /// Configures the lifetime of command pipeline components.
        /// </summary>
        /// <param name="lifetime">The <see cref="ComponentLifetime"/>.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        public CommandModelFacility WithLifetime(ComponentLifetime lifetime)
        {
            Lifetime = lifetime;
            return this;
        }

        /// <summary>
        /// Adds command handler to the container.
        /// </summary>
        /// <param name="handlerType">The type of the command handler.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="handlerType"/> is <c>null</c>.</exception>
        public CommandModelFacility WithHandler(Type handlerType)
        {
            Ensure.Arg.NotNull(handlerType, nameof(handlerType));
            ConfigureRegistry(
                r => r.TryAddEnumerable(
                    ComponentRegistration.Create(typeof(ICommandHandler<,>), handlerType, Lifetime)
                )
            );

            return this;
        }

        /// <summary>
        /// Adds command pre-handler to the container.
        /// </summary>
        /// <param name="handlerType">The type of the command handler.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="handlerType"/> is <c>null</c>.</exception>
        public CommandModelFacility WithPreHandler(Type handlerType)
        {
            Ensure.Arg.NotNull(handlerType, nameof(handlerType));
            ConfigureRegistry(
                r => r.TryAddEnumerable(
                    ComponentRegistration.Create(typeof(IPreCommandHandler<,>), handlerType, Lifetime)
                )
            );

            return this;
        }

        /// <summary>
        /// Adds command post-handler to the container.
        /// </summary>
        /// <param name="handlerType">The type of the command handler.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="handlerType"/> is <c>null</c>.</exception>
        public CommandModelFacility WithPostHandler(Type handlerType)
        {
            Ensure.Arg.NotNull(handlerType, nameof(handlerType));
            ConfigureRegistry(
                r => r.TryAddEnumerable(
                    ComponentRegistration.Create(typeof(IPostCommandHandler<,>), handlerType, Lifetime)
                )
            );

            return this;
        }

        /// <summary>
        /// Adds command pipeline behavior to the container.
        /// </summary>
        /// <param name="handlerType">The type of the command handler.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="handlerType"/> is <c>null</c>.</exception>
        public CommandModelFacility WithBehavior(Type handlerType)
        {
            Ensure.Arg.NotNull(handlerType, nameof(handlerType));
            ConfigureRegistry(
                r => r.TryAddEnumerable(
                    ComponentRegistration.Create(typeof(ICommandPipelineBehavior<,>), handlerType, Lifetime)
                )
            );

            return this;
        }

        /// <inheritdoc />
        protected override void Build(IComponentRegistry registry)
        {
            base.Build(registry);

            registry.TryAdd(
                new[]
                {
                    ComponentRegistration.Create<ICommandProcessor, CommandProcessor>(Lifetime),
                    ComponentRegistration.Singleton<ICommandDescriptorFactory, CommandDescriptorFactory>()
                });

            registry.TryAddEnumerable(
                new[]
                {
                    ComponentRegistration.Singleton<ICommandMetadataProvider, CancelableCommandMetadataProvider>(),
                    ComponentRegistration.Singleton(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(CancelableCommandBehavior<,>)),
                    ComponentRegistration.Create(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(PreCommandHandlerBehavior<,>)),
                    ComponentRegistration.Create(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(PostCommandHandlerBehavior<,>))
                });
        }
    }
}
