// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using System.Collections.Generic;
using AppCore.Commands.Metadata;
using FluentAssertions;
using Xunit;

namespace AppCore.Commands
{
    public class CommandContextTests
    {
        [Fact]
        public void CtorThrowsIfCommandDoesNotMatchDescriptorType()
        {
            Type commandType = typeof(TestCommand);
            var descriptor = new CommandDescriptor(commandType, new Dictionary<string, object>());

            Action action = () =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CommandContext<CancelableTestCommand, TestResult>(descriptor, new CancelableTestCommand());
            };

            action.Should()
                  .Throw<ArgumentException>();
        }

        [Fact]
        public void GetCommandReturnsCommand()
        {
            Type commandType = typeof(TestCommand);
            var descriptor = new CommandDescriptor(commandType, new Dictionary<string, object>());
            var command = new TestCommand();
            var context = new CommandContext<TestCommand, TestResult>(descriptor, command);

            context.Command.Should()
                   .BeSameAs(command);

            ((ICommandContext) context).Command.Should()
                                       .BeSameAs(command);
        }
    }
}
