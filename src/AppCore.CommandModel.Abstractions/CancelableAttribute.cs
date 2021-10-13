// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using JetBrains.Annotations;

namespace AppCore.CommandModel
{
    /// <summary>
    /// Enables cancellation for the command type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct|AttributeTargets.Class|AttributeTargets.Interface)]
    [BaseTypeRequired(typeof(ICommand<>))]
    public class CancelableAttribute : Attribute
    {
    }
}
