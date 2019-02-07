// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

#if NET452
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;

#endif

#if NETSTANDARD1_3 || NETSTANDARD2_0
using System.Threading;
#endif

namespace AppCore.Commands.Pipeline
{
    /// <summary>
    /// Default implementation of the <see cref="ICommandContextAccessor"/> interface.
    /// </summary>
    public class CommandContextAccessor : ICommandContextAccessor
    {
#if NET452
        private static readonly string _logicalDataKey = "__CommandContext__" + AppDomain.CurrentDomain.Id;

        /// <inheritdoc />
        public ICommandContext CommandContext
        {
            get
            {
                var handle = CallContext.LogicalGetData(_logicalDataKey) as ObjectHandle;
                return handle?.Unwrap() as ICommandContext;
            }
            set
            {
                CallContext.LogicalSetData(_logicalDataKey, new ObjectHandle(value));
            }
        }
#endif


#if NETSTANDARD1_3 || NETSTANDARD2_0
        private readonly AsyncLocal<ICommandContext> _commandContext = new AsyncLocal<ICommandContext>();

        /// <inheritdoc />
        public ICommandContext CommandContext
        {
            get => _commandContext.Value;
            set => _commandContext.Value = value;
        }
#endif
    }
}