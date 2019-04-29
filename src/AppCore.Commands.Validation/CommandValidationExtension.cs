// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection;
using AppCore.DependencyInjection.Facilities;

namespace AppCore.Commands.Validation
{
    /// <summary>
    /// Provides validation extension for the <see cref="ICommandsFacility"/>.
    /// </summary>
    public class CommandValidationExtension : FacilityExtension<ICommandsFacility>
    {
        /// <inheritdoc />
        protected override void RegisterComponents(IComponentRegistry registry, ICommandsFacility facility)
        {
            registry.Register(typeof(ICommandPipelineBehavior<,>))
                    .Add(typeof(CommandValidationBehavior<,>))
                    .WithLifetime(facility.Lifetime)
                    .IfNotRegistered();
        }
    }
}
