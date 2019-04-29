// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using AppCore.Commands;
using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection.Builder;
using AppCore.DependencyInjection.Facilities;
using AppCore.Diagnostics;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides extension methods to configure the <see cref="ICommandsFacility"/>.
    /// </summary>
    public static class CommandsRegistrationExtensions
    {
        /// <summary>
        /// Configure the default lifetime of command components.
        /// </summary>
        /// <param name="builder">The <see cref="IFacilityBuilder{TFacility}"/>.</param>
        /// <param name="lifetime">The default component lifetime.</param>
        /// <returns>The <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityBuilder<ICommandsFacility> WithLifetime(
            this IFacilityBuilder<ICommandsFacility> builder,
            ComponentLifetime lifetime)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));

            builder.Facility.Lifetime = lifetime;
            return builder;
        }

        /// <summary>
        /// Adds command handlers to the container.
        /// </summary>
        /// <param name="builder">The <see cref="IFacilityBuilder{TFacility}"/>.</param>
        /// <param name="registrationBuilder">A delegate to configure the <see cref="IRegistrationBuilder"/>.</param>
        /// <returns>The <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityBuilder<ICommandsFacility> AddHandlers(
            this IFacilityBuilder<ICommandsFacility> builder,
            Action<IRegistrationBuilder> registrationBuilder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));

            return builder.AddExtension(
                new RegistrationFacilityExtension<ICommandsFacility>(
                    typeof(ICommandHandler<,>),
                    (r,f) =>
                    {
                        r.WithDefaultLifetime(f.Lifetime);
                        registrationBuilder(r);
                    }));
        }

        /// <summary>
        /// Adds command pre-handlers to the container.
        /// </summary>
        /// <param name="builder">The <see cref="IFacilityBuilder{TFacility}"/>.</param>
        /// <param name="registrationBuilder">A delegate to configure the <see cref="IRegistrationBuilder"/>.</param>
        /// <returns>The <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityBuilder<ICommandsFacility> AddPreHandlers(
            this IFacilityBuilder<ICommandsFacility> builder,
            Action<IRegistrationBuilder> registrationBuilder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));

            return builder.AddExtension(
                new RegistrationFacilityExtension<ICommandsFacility>(
                    typeof(IPreCommandHandler<,>),
                    (r,f) =>
                    {
                        r.WithDefaultLifetime(f.Lifetime);
                        registrationBuilder(r);
                    }));
        }

        /// <summary>
        /// Adds command post-handlers to the container.
        /// </summary>
        /// <param name="builder">The <see cref="IFacilityBuilder{TFacility}"/>.</param>
        /// <param name="registrationBuilder">A delegate to configure the <see cref="IRegistrationBuilder"/>.</param>
        /// <returns>The <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityBuilder<ICommandsFacility> AddPostHandlers(
            this IFacilityBuilder<ICommandsFacility> builder,
            Action<IRegistrationBuilder> registrationBuilder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));

            return builder.AddExtension(
                new RegistrationFacilityExtension<ICommandsFacility>(
                    typeof(IPostCommandHandler<,>),
                    (r,f) =>
                    {
                        r.WithDefaultLifetime(f.Lifetime);
                        registrationBuilder(r);
                    }));
        }

        /// <summary>
        /// Adds command pipeline behaviors to the container.
        /// </summary>
        /// <param name="builder">The <see cref="IFacilityBuilder{TFacility}"/>.</param>
        /// <param name="registrationBuilder">A delegate to configure the <see cref="IRegistrationBuilder"/>.</param>
        /// <returns>The <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="builder"/> is <c>null</c>.</exception>
        public static IFacilityBuilder<ICommandsFacility> AddBehaviors(
            this IFacilityBuilder<ICommandsFacility> builder,
            Action<IRegistrationBuilder> registrationBuilder)
        {
            Ensure.Arg.NotNull(builder, nameof(builder));

            return builder.AddExtension(
                new RegistrationFacilityExtension<ICommandsFacility>(
                    typeof(ICommandPipelineBehavior<,>),
                    (r,f) =>
                    {
                        r.WithDefaultLifetime(f.Lifetime);
                        registrationBuilder(r);
                    }));
        }
    }
}
