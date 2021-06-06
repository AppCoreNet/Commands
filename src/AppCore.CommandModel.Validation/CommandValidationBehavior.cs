// Licensed under the MIT License.
// Copyright (c) 2018-2021 the AppCore .NET project.

using System.Threading;
using System.Threading.Tasks;
using AppCore.CommandModel.Pipeline;
using AppCore.Diagnostics;
using AppCore.ModelValidation;

namespace AppCore.CommandModel.Validation
{
    /// <summary>
    /// Provides a behavior for command validation.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TResult">The type of the result produced by the command.</typeparam>
    public class CommandValidationBehavior<TCommand, TResult> : ICommandPipelineBehavior<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly IValidator<TCommand> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidationBehavior{TCommand,TResult}"/> class.
        /// </summary>
        /// <param name="validator">The <see cref="IValidator{T}"/> used to validate the command.</param>
        public CommandValidationBehavior(IValidator<TCommand> validator)
        {
            Ensure.Arg.NotNull(validator, nameof(validator));
            _validator = validator;
        }

        /// <inheritdoc />
        public async Task ProcessAsync(
            ICommandContext<TCommand, TResult> context,
            CommandPipelineDelegate<TCommand, TResult> next,
            CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(context.Command, cancellationToken: cancellationToken)
                            .ConfigureAwait(false);

            await next(context, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}