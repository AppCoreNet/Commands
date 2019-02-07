// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using AppCore.Logging;

namespace AppCore.Commands.Logging
{
    /// <summary>
    /// Provides <see cref="LogEventId"/>'s for logged commands.
    /// </summary>
    public static class CommandLoggingBehaviorLogEventIds
    {
        /// <summary>
        /// Identifies the log event for a command which is about to be processed.
        /// </summary>
        public static readonly LogEventId CommandHandling = new LogEventId(0, nameof(CommandHandling));

        /// <summary>
        /// Identifies the log event for a command which has been successfully processed.
        /// </summary>
        public static readonly LogEventId CommandHandled = new LogEventId(0, nameof(CommandHandled));

        /// <summary>
        /// Identifies the log event for a command which could not be processed.
        /// </summary>
        public static readonly LogEventId CommandFailed = new LogEventId(0, nameof(CommandFailed));
    }
}