// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Commands.Pipeline
{
    internal interface ICommandPipeline<TResult>
    {
        Task<TResult> InvokeAsync(ICommandContext context, CancellationToken cancellationToken);
    }
}