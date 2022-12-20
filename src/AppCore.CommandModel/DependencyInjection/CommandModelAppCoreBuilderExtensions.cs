// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System;
using AppCore.CommandModel;
using AppCore.CommandModel.Metadata;
using AppCore.CommandModel.Pipeline;
using AppCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace AppCore.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to configure the <see cref="ICommandModelBuilder"/>.
    /// </summary>
    public static class CommandModelAppCoreBuilderExtensions
    {
        /// <summary>
        /// Adds the command model to the DI container.
        /// </summary>
        /// <param name="builder">The <see cref="IAppCoreBuilder"/>.</param>
        /// <param name="configure">The configure delegate.</param>
        /// <returns>The passed <see cref="IAppCoreBuilder"/>.</returns>
        public static IAppCoreBuilder AddCommandModel(
            this IAppCoreBuilder builder,
            Action<ICommandModelBuilder>? configure = null)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));

            IServiceCollection services = builder.Services;

            services.TryAdd(
                new[]
                {
                    ServiceDescriptor.Transient<ICommandProcessor, CommandProcessor>(),
                    ServiceDescriptor.Singleton<ICommandDescriptorFactory, CommandDescriptorFactory>()
                });

            services.TryAddEnumerable(
                new[]
                {
                    ServiceDescriptor.Singleton<ICommandMetadataProvider, CancelableCommandMetadataProvider>(),
                    ServiceDescriptor.Singleton(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(CancelableCommandBehavior<,>)),
                    ServiceDescriptor.Transient(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(PreCommandHandlerBehavior<,>)),
                    ServiceDescriptor.Transient(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(PostCommandHandlerBehavior<,>))
                });

            configure?.Invoke(new CommandModelBuilder(services));

            return builder;
        }
    }
}