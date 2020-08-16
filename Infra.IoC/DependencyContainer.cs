using Banking.Application;
using Banking.Data.Context;
using Banking.Data.Repository;
using Banking.Domain.Interfaces;
using Domain.Core.Bus;
using Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterBankingServices(this IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMqBus>();

            // Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();

            // Application
            services.AddTransient<IAccountService, AccountService>();
        }
    }
}