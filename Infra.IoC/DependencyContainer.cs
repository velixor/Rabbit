using Banking.Application.Services;
using Banking.Data.Context;
using Banking.Data.Repository;
using Banking.Domain.CommandHandlers;
using Banking.Domain.Commands;
using Banking.Domain.Interfaces;
using Domain.Core.Bus;
using Infra.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Transfer.Data.Context;

namespace Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterMicroservices(this IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMqBus>();

            RegisterBankingServices(services);
            RegisterTransferServices(services);
        }

        private static void RegisterTransferServices(IServiceCollection services)
        {
            services.AddTransient<TransferDbContext>();
        }

        private static void RegisterBankingServices(IServiceCollection services)
        {
            // Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();

            // Banking commands
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();

            // Application
            services.AddTransient<IAccountService, AccountService>();
        }
    }
}