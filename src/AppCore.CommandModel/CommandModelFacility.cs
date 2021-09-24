// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;
using AppCore.CommandModel;
using AppCore.CommandModel.Metadata;
using AppCore.CommandModel.Pipeline;
using AppCore.DependencyInjection.Facilities;
using AppCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        public ServiceLifetime Lifetime { get; private set; } = ServiceLifetime.Scoped;

        /// <summary>
        /// Registers the <see cref="ICommandContextAccessor"/> with the DI container.
        /// </summary>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        public CommandModelFacility WithEventContext()
        {
            ConfigureServices(services => services.TryAddSingleton<ICommandContextAccessor, CommandContextAccessor>());
            return this;
        }

        /// <summary>
        /// Configures the lifetime of command pipeline components.
        /// </summary>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/>.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        public CommandModelFacility WithLifetime(ServiceLifetime lifetime)
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
            ConfigureServices(
                services => services.TryAddEnumerable(
                    ServiceDescriptor.Describe(typeof(ICommandHandler<,>), handlerType, Lifetime)));

            return this;
        }

        /// <summary>
        /// Adds command handlers to the container.
        /// </summary>
        /// <param name="configure">The delegate used to configure the registration sources.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="configure"/> is <c>null</c>.</exception>
        public CommandModelFacility WithHandlersFrom(Action<IServiceDescriptorReflectionBuilder> configure)
        {
            Ensure.Arg.NotNull(configure, nameof(configure));
            ConfigureServices(services =>
            {
                services.TryAddEnumerableFrom(
                    typeof(ICommandHandler<,>),
                    builder =>
                    {
                        builder.WithDefaultLifetime(Lifetime);
                        configure(builder);
                    });
            });

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
            ConfigureServices(
                services => services.TryAddEnumerable(
                    ServiceDescriptor.Describe(typeof(IPreCommandHandler<,>), handlerType, Lifetime)));

            return this;
        }

        /// <summary>
        /// Adds command pre-handlers to the container.
        /// </summary>
        /// <param name="configure">The delegate used to configure the registration sources.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="configure"/> is <c>null</c>.</exception>
        public CommandModelFacility WithPreHandlersFrom(Action<IServiceDescriptorReflectionBuilder> configure)
        {
            Ensure.Arg.NotNull(configure, nameof(configure));
            ConfigureServices(services =>
            {
                services.TryAddEnumerableFrom(
                    typeof(IPreCommandHandler<,>),
                    builder =>
                    {
                        builder.WithDefaultLifetime(Lifetime);
                        configure(builder);
                    });
            });

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
            ConfigureServices(
                services => services.TryAddEnumerable(
                    ServiceDescriptor.Describe(typeof(IPostCommandHandler<,>), handlerType, Lifetime)));

            return this;
        }

        /// <summary>
        /// Adds command post-handlers to the container.
        /// </summary>
        /// <param name="configure">The delegate used to configure the registration sources.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="configure"/> is <c>null</c>.</exception>
        public CommandModelFacility WithPostHandlersFrom(Action<IServiceDescriptorReflectionBuilder> configure)
        {
            Ensure.Arg.NotNull(configure, nameof(configure));
            ConfigureServices(services =>
            {
                services.TryAddEnumerableFrom(
                    typeof(IPostCommandHandler<,>),
                    builder =>
                    {
                        builder.WithDefaultLifetime(Lifetime);
                        configure(builder);
                    });
            });

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
            ConfigureServices(
                services => services.TryAddEnumerable(
                    ServiceDescriptor.Describe(typeof(ICommandPipelineBehavior<,>), handlerType, Lifetime)));

            return this;
        }

        /// <summary>
        /// Adds command pipeline behaviors to the container.
        /// </summary>
        /// <param name="configure">The delegate used to configure the registration sources.</param>
        /// <returns>The <see cref="CommandModelFacility"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="configure"/> is <c>null</c>.</exception>
        public CommandModelFacility WithBehaviorsFrom(Action<IServiceDescriptorReflectionBuilder> configure)
        {
            Ensure.Arg.NotNull(configure, nameof(configure));
            ConfigureServices(services =>
            {
                services.TryAddEnumerableFrom(
                    typeof(ICommandPipelineBehavior<,>),
                    builder =>
                    {
                        builder.WithDefaultLifetime(Lifetime);
                        configure(builder);
                    });
            });

            return this;
        }

        /// <inheritdoc />
        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.TryAdd(
                new[]
                {
                    ServiceDescriptor.Describe(typeof(ICommandProcessor), typeof(CommandProcessor), Lifetime),
                    ServiceDescriptor.Singleton<ICommandDescriptorFactory, CommandDescriptorFactory>()
                });

            services.TryAddEnumerable(
                new[]
                {
                    ServiceDescriptor.Singleton<ICommandMetadataProvider, CancelableCommandMetadataProvider>(),
                    ServiceDescriptor.Singleton(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(CancelableCommandBehavior<,>)),
                    ServiceDescriptor.Describe(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(PreCommandHandlerBehavior<,>),
                        Lifetime),
                    ServiceDescriptor.Describe(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(PostCommandHandlerBehavior<,>),
                        Lifetime)
                });
        }
    }
}
