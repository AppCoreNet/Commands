// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;

namespace AppCore.Commands
{
    /// <summary>
    /// Represents a <see cref="Void"/> command result.
    /// </summary>
    /// <seealso cref="ICommand"/>
    public sealed class VoidResult : IEquatable<VoidResult>
    {
        /// <summary>
        /// Gets the singleton instance of <see cref="VoidResult"/>.
        /// </summary>
        public static readonly VoidResult Instance = new VoidResult();

        private VoidResult()
        {
        }

        /// <inheritdoc />
        public bool Equals(VoidResult other)
        {
            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is VoidResult other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 735275808;
        }

        public static bool operator ==(VoidResult left, VoidResult right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VoidResult left, VoidResult right)
        {
            return !Equals(left, right);
        }
    }
}