// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using AppCore.Logging;

namespace AppCore.Commands.Logging
{
    internal static class CommandLoggingBehaviorLoggerExtensions
    {
        private static readonly LoggerEventDelegate<object> _commandProcessing =
            LoggerEvent.Define<object>(
                LogLevel.Trace,
                CommandLoggingBehaviorLogEventIds.CommandHandling,
                "Processing command {command} ...");

        private static readonly LoggerEventDelegate<object, long, object> _commandProcessed =
            LoggerEvent.Define<object, long, object>(
                LogLevel.Debug,
                CommandLoggingBehaviorLogEventIds.CommandHandled,
                "Successfully processed command {command} in {commandProcessingTime} ms. Result is {commandResult}.");

        private static readonly LoggerEventDelegate<object> _commandFailed =
            LoggerEvent.Define<object>(
                LogLevel.Error,
                CommandLoggingBehaviorLogEventIds.CommandFailed,
                "Error processing command {command}.");

        public static void CommandProcessing(this ILogger logger, ICommandContext context)
        {
            _commandProcessing(logger, context.Command);
        }

        public static void CommandProcessed(this ILogger logger, ICommandContext context, TimeSpan processingTime)
        {
            _commandProcessed(logger, context.Command, (long) processingTime.TotalMilliseconds, context.Result);
        }

        public static void CommandFailed(this ILogger logger, ICommandContext context, Exception exception)
        {
            _commandFailed(logger, context.Command, exception: exception);
        }
    }
}