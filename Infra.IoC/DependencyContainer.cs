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
using Transfer.Application.Interfaces;
using Transfer.Application.Services;
using Transfer.Data.Context;
using Transfer.Data.Repository;
using Transfer.Domain.EventHandlers;
using Transfer.Domain.Events;

namespace Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterMicroservices(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus, RabbitMqBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMqBus(sp.GetRequiredService<IMediator>(), scopeFactory);
            });

            services.AddTransient<TransferCreatedEventHandler>();

            RegisterBankingServices(services);
            RegisterTransferServices(services);
        }

        private static void RegisterTransferServices(IServiceCollection services)
        {
            services.AddTransient<TransferDbContext>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferCreatedEventHandler>();
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