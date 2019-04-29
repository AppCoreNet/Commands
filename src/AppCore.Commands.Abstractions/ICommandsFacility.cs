// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using AppCore.DependencyInjection;
using AppCore.DependencyInjection.Facilities;

namespace AppCore.Commands
{
    /// <summary>
    /// Represents the commands facility.
    /// </summary>
    /// <seealso cref="IFacility"/>
    public interface ICommandsFacility : IFacility
    {
        /// <summary>
        /// Gets or sets the default lifetime of the components.
        /// </summary>
        ComponentLifetime Lifetime { get; set; }
    }
}
