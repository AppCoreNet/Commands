// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using System.Collections.Concurrent;
using AppCore.Commands.Metadata;

namespace AppCore.Commands
{
    internal static class CommandContextFactory
    {
        private static readonly Type _commandContextType = typeof(CommandContext<,>);

        private static readonly ConcurrentDictionary<Type, Delegate> _commandContextFactories =
            new ConcurrentDictionary<Type, Delegate>();

        private static Delegate GetCommandContextFactory(Type commandType)
        {
            return _commandContextFactories.GetOrAdd(
                commandType,
                t =>
                {
                    Type commandInterface = t.GetClosedTypeOf(typeof(ICommand<>));
                    Type commandContextType = _commandContextType.MakeGenericType(
                        t,
                        commandInterface.GenericTypeArguments[0]);
                    return TypeActivator.GetFactoryDelegate(commandContextType, typeof(CommandDescriptor), commandType);
                });
        }

        public static ICommandContext CreateCommandContext<TResult>(
            CommandDescriptor commandDescriptor,
            ICommand<TResult> command)
        {
            Delegate factory = GetCommandContextFactory(commandDescriptor.CommandType);
            return (ICommandContext) factory.DynamicInvoke(commandDescriptor, command);
        }
    }
}