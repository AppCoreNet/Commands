// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppCore.Commands.Metadata;
using AppCore.Commands.Pipeline;
using AppCore.DependencyInjection;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AppCore.Commands
{
    public class CommandProcessorTests
    {
        private readonly ICommandDescriptorFactory _commandDescriptorFactory;
        private readonly ICommandContextAccessor _commandContextAccessor;
        private readonly IContainer _container;
        private readonly CommandProcessor _processor;
        private ICommandHandler<TestCommand, TestResult> _commandHandler;
        private IEnumerable<ICommandPipelineBehavior<TestCommand, TestResult>> _commandBehaviors;

        public CommandProcessorTests()
        {
            _commandHandler = Substitute.For<ICommandHandler<TestCommand, TestResult>>();
            _commandBehaviors = Enumerable.Empty<ICommandPipelineBehavior<TestCommand, TestResult>>();

            _commandDescriptorFactory = Substitute.For<ICommandDescriptorFactory>();
            _commandDescriptorFactory.CreateDescriptor(Arg.Is(typeof(TestCommand)))
                                     .Returns(
                                         new CommandDescriptor(typeof(TestCommand), new Dictionary<string, object>()));

            _commandContextAccessor = Substitute.For<ICommandContextAccessor>();

            _container = Substitute.For<IContainer>();
            _container.Resolve(Arg.Is(typeof(ICommandHandler<TestCommand, TestResult>)))
                      .Returns(_ => _commandHandler);

            _container.Resolve(Arg.Is(typeof(IEnumerable<ICommandPipelineBehavior<TestCommand, TestResult>>)))
                      .Returns(_ => _commandBehaviors);

            _processor = new CommandProcessor(_container, _commandDescriptorFactory, _commandContextAccessor);
        }

        [Fact]
        public async Task ProcessAsyncInvokesCommandHandler()
        {
            var ct = new CancellationToken();
            var cmd = new TestCommand();

            await _processor.ProcessAsync(cmd, ct);

            await _commandHandler.Received()
                                 .HandleAsync(cmd, ct);
        }

        [Fact]
        public async Task ProcessAsyncUpdatesCommandContextAccessor()
        {
            var ct = new CancellationToken();
            var cmd = new TestCommand();

            await _processor.ProcessAsync(cmd, ct);

            await _commandHandler.Received()
                                 .HandleAsync(cmd, ct);

            _commandContextAccessor.Received()
                                   .CommandContext = Arg.Any<ICommandContext<TestCommand, TestResult>>();

            _commandContextAccessor.Received()
                                   .CommandContext = null;
        }

        [Fact]
        public async Task ProcessAsyncReturnsResultOfCommandHandler()
        {
            var expectedResult = new TestResult();

            _commandHandler.HandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>())
                           .Returns(expectedResult);

            TestResult result = await _processor.ProcessAsync(new TestCommand(), CancellationToken.None);
            result.Should()
                  .BeSameAs(expectedResult);
        }

        [Fact]
        public void ProcessAsyncThrowsIfCommandHandlerFailed()
        {
            _commandHandler.HandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>())
                           .Throws(new Exception());

            Func<Task> process = async () => await _processor.ProcessAsync(new TestCommand(), CancellationToken.None);
            process.Should()
                   .Throw<Exception>();
        }

        [Fact]
        public void ProcessAsyncThrowsIfCommandBehaviorFailed()
        {
            var behavior = Substitute.For<ICommandPipelineBehavior<TestCommand, TestResult>>();

            behavior.ProcessAsync(
                        Arg.Any<ICommandContext<TestCommand, TestResult>>(),
                        Arg.Any<CommandPipelineDelegate<TestCommand, TestResult>>(),
                        Arg.Any<CancellationToken>())
                    .Returns(
                        async ci =>
                        {
                            var context = ci.ArgAt<ICommandContext<TestCommand, TestResult>>(0);
                            var next = ci.ArgAt<CommandPipelineDelegate<TestCommand, TestResult>>(1);
                            var ct = ci.ArgAt<CancellationToken>(2);

                            context.Fail(new Exception());
                            await next(context, ct);
                        });

            _commandBehaviors = new[] {behavior};

            Func<Task> process = async () => await _processor.ProcessAsync(new TestCommand(), CancellationToken.None);
            process.Should()
                   .Throw<Exception>();
        }
    }
}
