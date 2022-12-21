using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace AppCore.CommandModel;

#nullable disable
public class ServiceCollection : List<ServiceDescriptor>, IServiceCollection
{
}
#nullable restore
