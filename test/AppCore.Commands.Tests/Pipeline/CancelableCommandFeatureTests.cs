// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System.Threading;
using FluentAssertions;
using Xunit;

namespace AppCore.Commands.Pipeline
{
    public class CancelableCommandFeatureTests
    {
        [Fact]
        public void CancelSignalsCancellationTokenSource()
        {
            var cts = new CancellationTokenSource();
            var feature = new CancelableCommandFeature(cts);
            feature.Cancel();

            cts.IsCancellationRequested.Should()
               .BeTrue();
        }
    }
}
