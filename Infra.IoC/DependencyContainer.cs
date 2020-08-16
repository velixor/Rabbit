using Domain.Core.Bus;
using Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMqBus>();
        }
    }
}