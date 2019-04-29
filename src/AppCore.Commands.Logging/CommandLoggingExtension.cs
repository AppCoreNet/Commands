// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection;
using AppCore.DependencyInjection.Facilities;

namespace AppCore.Commands.Logging
{
    /// <summary>
    /// Provides logging extension for the <see cref="ICommandsFacility"/>.
    /// </summary>
    public class CommandLoggingExtension : FacilityExtension<ICommandsFacility>
    {
        /// <inheritdoc />
        protected override void RegisterComponents(IComponentRegistry registry, ICommandsFacility facility)
        {
            registry.Register(typeof(ICommandPipelineBehavior<,>))
                    .Add(typeof(CommandLoggingBehavior<,>))
                    .PerContainer()
                    .IfNotRegistered();
        }
    }
}
