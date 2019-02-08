// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System.Threading;
using System.Threading.Tasks;
using AppCore.Commands.Metadata;

namespace AppCore.Commands.Pipeline
{
    internal interface ICommandPipeline<TResult>
    {
        ICommandContext CreateCommandContext(CommandDescriptor descriptor, ICommand<TResult> command);

        Task<TResult> InvokeAsync(ICommandContext context, CancellationToken cancellationToken);
    }
}