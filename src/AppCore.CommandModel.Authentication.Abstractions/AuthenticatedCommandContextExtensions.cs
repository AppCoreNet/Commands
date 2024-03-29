// Licensed under the MIT License.
// Copyright (c) 2020 the AppCore .NET project.

using System.Security.Principal;
using AppCore.CommandModel.Pipeline;
using AppCore.Diagnostics;

namespace AppCore.CommandModel;

/// <summary>
/// Provides authorization related extension methods for the <see cref="ICommandContext"/>.
/// </summary>
public static class AuthenticatedCommandContextExtensions
{
    /// <summary>
    /// Gets a value indicating whether a user is authenticated.
    /// </summary>
    /// <param name="context">The <see cref="ICommandContext"/>.</param>
    /// <returns><c>true</c> if a user is authenticated; <c>false</c> otherwise.</returns>
    public static bool IsAuthenticated(this ICommandContext context)
    {
        Ensure.Arg.NotNull(context);
        return context.User().Identity.IsAuthenticated;
    }

    /// <summary>
    /// Gets the current <see cref="IPrincipal"/> associated with the context.
    /// </summary>
    /// <param name="context">The <see cref="ICommandContext"/>.</param>
    /// <returns>The current <see cref="IPrincipal"/>.</returns>
    public static IPrincipal User(this ICommandContext context)
    {
        Ensure.Arg.NotNull(context);
        var feature = context.GetFeature<IAuthenticatedCommandFeature>();
        return feature.User;
    }
}