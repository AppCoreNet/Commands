// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Commands.Pipeline
{
    public delegate Task CommandPipelineDelegate<in TCommand, TResult>(
        ICommandContext<TCommand, TResult> command,
        CancellationToken cancellationToken)
        where TCommand : ICommand<TResult>;
}