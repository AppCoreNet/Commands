// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using Microsoft.Extensions.Logging;

namespace AppCore.CommandModel.Logging
{
    /// <summary>
    /// Provides <see cref="EventId"/>'s for logged commands.
    /// </summary>
    public static class CommandLoggingBehaviorLogEventIds
    {
        /// <summary>
        /// Identifies the log event for a command which is about to be processed.
        /// </summary>
        public static readonly EventId CommandHandling = new EventId(0, nameof(CommandHandling));

        /// <summary>
        /// Identifies the log event for a command which has been successfully processed.
        /// </summary>
        public static readonly EventId CommandHandled = new EventId(0, nameof(CommandHandled));

        /// <summary>
        /// Identifies the log event for a command which could not be processed.
        /// </summary>
        public static readonly EventId CommandFailed = new EventId(0, nameof(CommandFailed));
    }
}