using AppCore.CommandModel.Metadata;
using AppCore.CommandModel.Pipeline;
using AppCore.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AppCore.CommandModel;

public class CommandModelRegistrationTests
{
    [Fact]
    public void AddCommandModelRegistersCoreServices()
    {
        var services = new ServiceCollection();

        services.AddAppCore()
                .AddCommandModel();

        services.Should()
                .ContainEquivalentOf(ServiceDescriptor.Transient<ICommandProcessor, CommandProcessor>());

        services.Should()
                .ContainEquivalentOf(
                    ServiceDescriptor.Singleton<ICommandDescriptorFactory, CommandDescriptorFactory>());

        services.Should()
                .ContainEquivalentOf(
                    ServiceDescriptor.Singleton<ICommandMetadataProvider, CancelableCommandMetadataProvider>());

        services.Should()
                .ContainEquivalentOf(
                    ServiceDescriptor.Singleton(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(CancelableCommandBehavior<,>)));

        services.Should()
                .ContainEquivalentOf(
                    ServiceDescriptor.Transient(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(PreCommandHandlerBehavior<,>)));

        services.Should()
                .ContainEquivalentOf(
                    ServiceDescriptor.Transient(
                        typeof(ICommandPipelineBehavior<,>),
                        typeof(PostCommandHandlerBehavior<,>)));
    }

    [Fact]
    public void WithCommandContextRegistersContextAccessor()
    {
        var services = new ServiceCollection();

        services.AddAppCore()
                .AddCommandModel(c => c.AddCommandContext());

        services.Should()
                .ContainEquivalentOf(ServiceDescriptor.Singleton<ICommandContextAccessor, CommandContextAccessor>());
    }
}