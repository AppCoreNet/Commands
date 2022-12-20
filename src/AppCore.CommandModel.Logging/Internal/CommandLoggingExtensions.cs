// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using Microsoft.Extensions.Logging;

namespace AppCore.CommandModel.Logging;

internal static class CommandLoggingExtensions
{
    private static readonly Action<ILogger, object, Exception?> _commandProcessing =
        LoggerMessage.Define<object>(
            LogLevel.Trace,
            CommandLoggingBehaviorLogEventIds.CommandHandling,
            "Processing command {@command} ...");

    private static readonly Action<ILogger, object, long, object?, Exception?> _commandProcessed =
        LoggerMessage.Define<object, long, object?>(
            LogLevel.Debug,
            CommandLoggingBehaviorLogEventIds.CommandHandled,
            "Successfully processed command {@command} in {commandProcessingTime} ms. Result is {@commandResult}.");

    private static readonly Action<ILogger, object, Exception?> _commandFailed =
        LoggerMessage.Define<object>(
            LogLevel.Error,
            CommandLoggingBehaviorLogEventIds.CommandFailed,
            "Error processing command {@command}.");

    public static void CommandProcessing(this ILogger logger, ICommandContext context)
    {
        _commandProcessing(logger, context.Command, null);
    }

    public static void CommandProcessed(this ILogger logger, ICommandContext context, TimeSpan processingTime)
    {
        _commandProcessed(logger, context.Command, (long) processingTime.TotalMilliseconds, context.Result, null);
    }

    public static void CommandFailed(this ILogger logger, ICommandContext context, Exception exception)
    {
        _commandFailed(logger, context.Command, exception);
    }
}