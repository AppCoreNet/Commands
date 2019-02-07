// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using System.Collections.Concurrent;
using AppCore.DependencyInjection;

namespace AppCore.Commands.Pipeline
{
    internal static class CommandPipelineFactory
    {
        private static readonly Type _commandPipelineType = typeof(CommandPipeline<,>);

        private static readonly ConcurrentDictionary<Type, Type> _commandPipelineTypes =
            new ConcurrentDictionary<Type, Type>();

        private static Type GetCommandPipelineType(Type commandType)
        {
            return _commandPipelineTypes.GetOrAdd(commandType, t =>
            {
                Type commandInterfaceType = t.GetClosedTypeOf(typeof(ICommand<>));
                return _commandPipelineType.MakeGenericType(t, commandInterfaceType.GenericTypeArguments[0]);
            });
        }

        public static object CreateCommandPipeline(Type commandType, IContainer container)
        {
            Type commandPipelineType = GetCommandPipelineType(commandType);
            return commandPipelineType.CreateInstance(container);
        }
    }
}