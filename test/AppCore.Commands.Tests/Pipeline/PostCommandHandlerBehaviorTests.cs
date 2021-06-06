// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppCore.CommandModel.Metadata;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AppCore.CommandModel.Pipeline
{
    public class PostCommandHandlerBehaviorTests
    {
        [Fact]
        public async Task InvokesHandlersAfterNext()
        {
            var invokeOrder = new List<object>();
            
            var handlers = new[]
            {
                Substitute.For<IPostCommandHandler<TestCommand, TestResult>>(),
                Substitute.For<IPostCommandHandler<TestCommand, TestResult>>()
            };

            handlers[0]
                .When(
                    h => h.OnHandledAsync(
                        Arg.Any<ICommandContext<TestCommand, TestResult>>(),
                        Arg.Any<CancellationToken>()))
                .Do(_ => invokeOrder.Add(handlers[0]));

            handlers[1]
                .When(
                    h => h.OnHandledAsync(
                        Arg.Any<ICommandContext<TestCommand, TestResult>>(),
                        Arg.Any<CancellationToken>()))
                .Do(_ => invokeOrder.Add(handlers[1]));

            var next = Substitute.For<CommandPipelineDelegate<TestCommand, TestResult>>();
            next.When(n => n.Invoke(Arg.Any<ICommandContext<TestCommand, TestResult>>(), Arg.Any<CancellationToken>()))
                .Do(_ => { invokeOrder.Add(next); });

            var command = new TestCommand();
            var context = new CommandContext<TestCommand, TestResult>(
                new CommandDescriptor(typeof(TestCommand), new Dictionary<string, object>()),
                command);

            var ct = new CancellationToken();

            var behavior = new PostCommandHandlerBehavior<TestCommand, TestResult>(handlers);
            await behavior.ProcessAsync(context, next, ct);

            await next.Received(1)
                      .Invoke(Arg.Is<ICommandContext<TestCommand, TestResult>>(context), ct);

            invokeOrder[0]
                .Should()
                .Be(next);

            await handlers[0]
                  .Received(1)
                  .OnHandledAsync(
                      Arg.Is<ICommandContext<TestCommand, TestResult>>(context),
                      ct);

            invokeOrder[1]
                .Should()
                .Be(handlers[0]);

            await handlers[1]
                  .Received(1)
                  .OnHandledAsync(
                      Arg.Is<ICommandContext<TestCommand, TestResult>>(context),
                      ct);

            invokeOrder[2]
                .Should()
                .Be(handlers[1]);

        }
    }
}