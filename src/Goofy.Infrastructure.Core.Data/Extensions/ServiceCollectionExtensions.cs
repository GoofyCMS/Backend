﻿using System;
using System.Reflection;
using Goofy.Domain.Core.Service.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Goofy.Infrastructure.Core.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddUnitOfWork<TUoW>(this IServiceCollection services) where TUoW : IUnitOfWork
        {
            return services.AddUnitOfWork(typeof(TUoW));
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, Type unitOfWorkType)
        {
            if (typeof(IUnitOfWork).IsAssignableFrom(unitOfWorkType))
            {
                services.AddScoped(unitOfWorkType,
                    s =>
                    {
                        var dataAccessConfig = s.GetRequiredService<IOptions<DataAccessConfiguration>>();
                        return ActivatorUtilities.CreateInstance(s, unitOfWorkType, new string[] { dataAccessConfig.Value.ConnectionString });
                    });
            }
            else
            {
                throw new Exception();
            }
            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, Type iUnitOfWorkInterface, Type unitOfWorkObjectType)
        {
            var interfaceTypeInfo = iUnitOfWorkInterface.GetTypeInfo();
            if (!interfaceTypeInfo.IsInterface || !typeof(IUnitOfWork).IsAssignableFrom(iUnitOfWorkInterface))
            {
                throw new ArgumentException("Invalid interface type for service IUnitOfWork");
            }
            if (!interfaceTypeInfo.IsAssignableFrom(unitOfWorkObjectType))
            {
                throw new ArgumentException("Invalid UnitOfWork type");
            }

            if (typeof(IUnitOfWork).IsAssignableFrom(unitOfWorkObjectType))
            {
                services.AddUnitOfWork(unitOfWorkObjectType);
                services.AddScoped(iUnitOfWorkInterface, s => s.GetRequiredService(unitOfWorkObjectType));
            }
            else
            {
                throw new Exception("Invalid UnitOfWorkType");
            }

            return services;
        }
    }
}
