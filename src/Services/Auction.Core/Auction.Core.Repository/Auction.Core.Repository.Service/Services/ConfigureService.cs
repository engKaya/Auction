﻿using Auction.Core.Base.Common.Interfaces.ServiceConfigurers;
using Auction.Core.Repository.Common.Interface;
using Auction.Core.Repository.Common.Interface.Repository;
using Auction.Core.Repository.Service.Services.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Core.Repository.Service.Services
{
    internal class ConfigureService : IServiceConfigurer
    {
        public void Configure(IServiceCollection services)
        { 
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>)); 
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
