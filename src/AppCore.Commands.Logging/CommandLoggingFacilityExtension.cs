// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using AppCore.Commands.Logging;
using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection.Facilities;

// ReSharper disable once CheckNamespace
namespace AppCore.DependencyInjection
{
    /// <summary>
    /// Provides logging extension for the <see cref="ICommandsFacility"/>.
    /// </summary>
    public class CommandLoggingFacilityExtension : FacilityExtension<ICommandsFacility>
    {
        protected override void RegisterComponents(IComponentRegistry registry, ICommandsFacility facility)
        {
            registry.Register(typeof(ICommandPipelineBehavior<,>))
                    .Add(typeof(CommandLoggingBehavior<,>))
                    .PerContainer()
                    .IfNotRegistered();
        }
    }
}
