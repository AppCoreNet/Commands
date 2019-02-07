// Licensed under the MIT License.
// Copyright (c) 2018 the AppCore .NET project.

using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AppCore.Commands
{
    public class CommandContextExtensionsTests
    {
        public class TestFeature
        {
        }

        private readonly ICommandContext<TestCommand, TestResult> _context;
        private readonly Dictionary<Type, object> _features;

        public CommandContextExtensionsTests()
        {
            _context = Substitute.For<ICommandContext<TestCommand, TestResult>>();
            _features = new Dictionary<Type, object>();
            _context.Features.Returns(_features);
        }

        [Fact]
        public void AddFeatureAddsByType()
        {
            var feature = new TestFeature();
            _context.AddFeature(feature);

            _features.Should()
                     .Contain(typeof(TestFeature), feature);
        }

        [Fact]
        public void AddFeatureThrowsForDuplicateFeature()
        {
            var feature = new TestFeature();
            _context.AddFeature(feature);

            Action action = () => { _context.AddFeature(feature); };
            action.Should()
                  .Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetFeatureReturnsRegisteredFeature()
        {
            var feature = new TestFeature();
            _features.Add(typeof(TestFeature), feature);

            _context.GetFeature<TestFeature>()
                    .Should()
                    .BeSameAs(feature);
        }

        [Fact]
        public void GetFeatureThrowsForUnknownFeature()
        {
            Action action = () => { _context.GetFeature<TestFeature>(); };
            action.Should()
                  .Throw<InvalidOperationException>();
        }

        [Fact]
        public void HasFeatureReturnsTrueForRegisteredFeature()
        {
            var feature = new TestFeature();
            _features.Add(typeof(TestFeature), feature);

            _context.HasFeature<TestFeature>()
                    .Should()
                    .BeTrue();
        }

        [Fact]
        public void HasFeatureReturnFalseForUnknownFeature()
        {
            _context.HasFeature<TestFeature>()
                    .Should()
                    .BeFalse();
        }
    }
}