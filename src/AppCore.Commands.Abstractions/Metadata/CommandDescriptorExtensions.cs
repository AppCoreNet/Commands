// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using System.Collections.Generic;
using AppCore.Diagnostics;

namespace AppCore.Commands.Metadata
{
    /// <summary>
    /// Provides extension methods for the <see cref="CommandDescriptor"/> type.
    /// </summary>
    public static class CommandDescriptorExtensions
    {
        public static bool TryGetMetadata<T>(this CommandDescriptor descriptor, string key, out T value)
        {
            Ensure.Arg.NotNull(descriptor, nameof(descriptor));
            Ensure.Arg.NotEmpty(key, nameof(key));

            if (!descriptor.Metadata.TryGetValue(key, out object tmp))
            {
                value = default(T);
                return false;
            }

            if (!(tmp is T))
            {
                throw new InvalidCastException(
                    $"Metadata value with key {key} is not of the expected type {typeof(T).GetDisplayName()}");
            }

            value = (T) tmp;
            return true;
        }

        public static T GetMetadata<T>(this CommandDescriptor descriptor, string key, T defaultValue)
        {
            if (!TryGetMetadata(descriptor, key, out T result))
                result = defaultValue;

            return result;
        }

        public static T GetMetadata<T>(this CommandDescriptor descriptor, string key)
        {
            if (!TryGetMetadata(descriptor, key, out T value))
                throw new KeyNotFoundException($"Metadata value with key {key} is unknown.");

            return value;
        }
    }
}
