// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection;
using AppCore.DependencyInjection.Facilities;

namespace AppCore.Commands.Extensions
{
    /// <summary>
    /// Represents extension for the <see cref="ICommandsFacility"/> which registers the <see cref="ICommandContextAccessor"/>.
    /// </summary>
    public class CommandContextExtension : FacilityExtension<ICommandsFacility>
    {
        /// <inheritdoc />
        protected override void RegisterComponents(IComponentRegistry registry, ICommandsFacility facility)
        {
            registry.Register<ICommandContextAccessor>()
                    .Add<CommandContextAccessor>()
                    .PerContainer()
                    .IfNoneRegistered();
        }
    }
}