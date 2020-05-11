// Licensed under the MIT License.
// Copyright (c) 2020 the AppCore .NET project.

using System;

namespace AppCore.Commands
{
    /// <summary>
    /// When applied to a command type, requires authorization of the user.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeAttribute : Attribute
    {
    }
}