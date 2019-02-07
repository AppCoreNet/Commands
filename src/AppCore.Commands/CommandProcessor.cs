// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using System.Threading;
using System.Threading.Tasks;
using AppCore.Commands.Metadata;
using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection;
using AppCore.Diagnostics;

namespace AppCore.Commands
{
    /// <summary>
    /// Provides the default command processor implementation.
    /// </summary>
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandDescriptorFactory _commandDescriptorFactory;
        private readonly ICommandContextAccessor _commandContextAccessor;
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandProcessor"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> used to resolve handlers and behaviors.</param>
        /// <param name="commandDescriptorFactory">The factory for <see cref="CommandDescriptor"/>'s.</param>
        /// <param name="commandContextAccessor">The accessor for the current <see cref="ICommandContext"/>.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="container"/> is <c>null</c>.</exception>
        public CommandProcessor(
            IContainer container,
            ICommandDescriptorFactory commandDescriptorFactory,
            ICommandContextAccessor commandContextAccessor = null)
        {
            Ensure.Arg.NotNull(commandDescriptorFactory, nameof(commandDescriptorFactory));
            Ensure.Arg.NotNull(container, nameof(container));

            _commandDescriptorFactory = commandDescriptorFactory;
            _commandContextAccessor = commandContextAccessor;
            _container = container;
        }

        /// <inheritdoc />
        public async Task<TResult> ProcessAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken)
        {
            Ensure.Arg.NotNull(command, nameof(command));

            Type commandType = command.GetType();
            CommandDescriptor commandDescriptor = _commandDescriptorFactory.CreateDescriptor(commandType);
            ICommandContext commandContext = CommandContextFactory.CreateCommandContext(commandDescriptor, command);

            if (_commandContextAccessor != null)
                _commandContextAccessor.CommandContext = commandContext;

            try
            {
                var pipeline =
                    (ICommandPipeline<TResult>) CommandPipelineFactory.CreateCommandPipeline(commandType, _container);

                return await pipeline.InvokeAsync(commandContext, cancellationToken)
                                     .ConfigureAwait(false);
            }
            finally
            {
                if (_commandContextAccessor != null)
                    _commandContextAccessor.CommandContext = null;
            }
        }
    }
}