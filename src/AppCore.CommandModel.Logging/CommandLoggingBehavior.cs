// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AppCore.CommandModel.Pipeline;
using AppCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace AppCore.CommandModel.Logging
{
    /// <summary>
    /// Provides a pipeline behavior which logs commands.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class CommandLoggingBehavior<TCommand, TResult> : ICommandPipelineBehavior<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoggingBehavior{TCommand, TResult}"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used to log events.</param>
        public CommandLoggingBehavior(ILogger<ICommandProcessor> logger)
        {
            Ensure.Arg.NotNull(logger, nameof(logger));
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task ProcessAsync(
            ICommandContext<TCommand, TResult> context,
            CommandPipelineDelegate<TCommand, TResult> next,
            CancellationToken cancellationToken)
        {
            _logger.CommandProcessing(context);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                await next(context, cancellationToken)
                    .ConfigureAwait(false);

                stopwatch.Stop();

                if (!context.IsFailed)
                {
                    _logger.CommandProcessed(context, stopwatch.Elapsed);
                }
                else
                {
                    _logger.CommandFailed(context, context.Error);
                }
            }
            catch (Exception exception)
            {
                _logger.CommandFailed(context, exception);
                throw;
            }
        }
    }
}
