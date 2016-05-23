﻿using Goofy.Domain.Core.Entity;
using Goofy.Application.Core.Service;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Core.Service.Data;
using Goofy.Domain.Plugins.Service.Data;

namespace Goofy.Application.Plugins.Services
{
    public class PluginServiceMapper<TEntity, TViewModel> : ServiceMapper<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class
    {
        public PluginServiceMapper(IPluginUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IRepository<TEntity> repository = null)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
