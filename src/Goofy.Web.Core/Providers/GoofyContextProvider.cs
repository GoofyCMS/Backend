﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Core.Service.Data;
using Goofy.Domain.Core;

namespace Goofy.Web.Core.Providers
{
    public abstract class BaseContextProvider<TContext> : ContextProvider where TContext : class, new()
    {
        private readonly IUnitOfWork _context;
        private readonly ITypeAdapterFactory _typeAdapterFactory;
        private readonly EFContextProvider<TContext> _contextProvider;

        protected BaseContextProvider(IUnitOfWork context, ITypeAdapterFactory typeAdapterFactory)
        {
            _context = context;
            _typeAdapterFactory = typeAdapterFactory;
            _contextProvider = new EFContextProvider<TContext>();
        }

        [FromServices]
        public IServiceProvider ServiceProvider { get; set; }

        public override IDbConnection GetDbConnection()
        {
            return _contextProvider.GetDbConnection();
        }

        protected override void OpenDbConnection()
        {
            var entityConnection = ((IObjectContextAdapter)_context).ObjectContext.Connection as EntityConnection;
            if (entityConnection == null || entityConnection.State != ConnectionState.Closed)
                return;
            entityConnection.Open();
        }

        protected override void CloseDbConnection()
        {
            var entityConnection = ((IObjectContextAdapter)_context)?.ObjectContext.Connection as EntityConnection;
            if (entityConnection == null)
                return;
            entityConnection.Close();
            entityConnection.Dispose();
        }

        protected override string BuildJsonMetadata()
        {
            return _contextProvider.Metadata();
        }

        protected override void SaveChangesCore(SaveWorkState saveWorkState)
        {
            try
            {
                saveWorkState.KeyMappings = new List<KeyMapping>();

                var entitiesToUpdate = new List<Tuple<dynamic, object, object>>();

                var serviceMaps = new Dictionary<string, dynamic>();
                foreach (var map in saveWorkState.SaveMap)
                {
                    var type = map.Key;
                    foreach (var info in map.Value)
                    {
                        dynamic serviceMapper;
                        if (serviceMaps.ContainsKey(type.FullName))
                        {
                            serviceMapper = serviceMaps[type.FullName];
                        }
                        else
                        {
                            serviceMapper = FindRepository(type);
                            serviceMaps.Add(type.FullName, serviceMapper);
                        }

                        switch (info.EntityState)
                        {
                            case EntityState.Added:
                                {
                                    var entity = serviceMapper.Add((dynamic)info.Entity);
                                    entitiesToUpdate.Add(new Tuple<dynamic, object, object>(serviceMapper, entity,
                                        info.Entity));
                                    break;
                                }
                            case EntityState.Deleted:
                                {
                                    var entity = serviceMapper.Remove((dynamic)info.Entity);
                                    //entitiesToUpdate.Add(new Tuple<dynamic, object, object>(serviceMapper, entity,
                                    //    info.Entity));
                                    break;
                                }
                            case EntityState.Detached:
                                {
                                    break;
                                }
                            case EntityState.Modified:
                                {
                                    var entity = serviceMapper.Modify((dynamic)info.Entity);
                                    entitiesToUpdate.Add(new Tuple<dynamic, object, object>(serviceMapper, entity,
                                        info.Entity));
                                    break;
                                }
                            case EntityState.Unchanged:
                                {
                                    break;
                                }
                        }
                    }
                }

                // Fetch Temp Keys
                foreach (var entitiesWithAutoGeneratedKey in saveWorkState.EntitiesWithAutoGeneratedKeys)
                {
                    entitiesWithAutoGeneratedKey.AutoGeneratedKey.TempValue = entitiesWithAutoGeneratedKey
                        .AutoGeneratedKey.TempValue ?? entitiesWithAutoGeneratedKey.Entity.GetEntityKeys().First();
                }

                // Save to Database
                _context.SaveChanges();

                // Update ViewModels from updated models
                foreach (var tuple in entitiesToUpdate)
                {
                    try
                    {
                        tuple.Item1.UpdateViewmodel((dynamic)tuple.Item2, (dynamic)tuple.Item3, reload: true);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Mapping exception: {tuple.Item3.GetType().Name}, {ex.Message}");
                    }
                }

                // Update key mappings
                saveWorkState.KeyMappings =
                    saveWorkState.EntitiesWithAutoGeneratedKeys.Where(s => s.Entity != null)
                        .Select(entityInfo => new KeyMapping
                        {
                            EntityTypeName = entityInfo.Entity.GetType().FullName,
                            TempValue = entityInfo.AutoGeneratedKey.TempValue,
                            RealValue = entityInfo.Entity.GetEntityKeys().First()
                        }).ToList();
            }
            catch (Exception ex)
            {
                var builder = new StringBuilder();
                throw new Exception(builder.ToString(), ex);
            }
        }

        private dynamic FindRepository(Type type)
        {
            /*
               TODO: Fix this
            */
            //var sourceType = _typeAdapterFactory.GetSourceTypes(type).FirstOrDefault();
            //if (sourceType == null)
            //    throw new Exception($"Map for '{type.Name}' not found");

            //var repositoryType = typeof(IRepository<>).MakeGenericType(sourceType);
            //var repository = ServiceProvider.GetRequiredService(repositoryType);

            //var repository = Container.Resolve(repositoryType, _name,
            //    new ParameterOverride("unitOfWork", _context));
            //if (repository == null)
            //    throw new Exception($"Repository for '{type.Name}' not found");

            //var serviceType = typeof(IServiceMapper<,>).MakeGenericType(sourceType, type);
            //var serviceMapper = Container.Resolve(serviceType, _name,
            //    new ParameterOverride("unitOfWork", _context), new ParameterOverride("repository", repository));
            //if (serviceMapper == null)
            //    throw new Exception($"Service mapper for '{type.Name}' not found");

            //return serviceMapper;
            return null;
        }
    }
}
