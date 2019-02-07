// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System.Threading;
using System.Threading.Tasks;
using AppCore.Commands.Pipeline;
using AppCore.Diagnostics;
using AppCore.Validation;

namespace AppCore.Commands.Validation
{
    public class CommandValidationBehavior<TCommand, TResult> : ICommandPipelineBehavior<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        private readonly IValidator<TCommand> _validator;

        public CommandValidationBehavior(IValidator<TCommand> validator)
        {
            Ensure.Arg.NotNull(validator, nameof(validator));
            _validator = validator;
        }

        public async Task ProcessAsync(
            ICommandContext<TCommand, TResult> context,
            CommandPipelineDelegate<TCommand, TResult> next,
            CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(context.Command, cancellationToken: cancellationToken);

            await next(context, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}