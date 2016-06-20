using System;
using System.Collections.Generic;
using System.Reflection;
using Goofy.Application.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.SimpleExternalComment.Service.Adapter;
using Goofy.Domain.SimpleExternalComment.Entity;
using Goofy.Security.Extensions;
using Goofy.Domain.SimpleExternalComment.Service.Data;
using Goofy.Infrastructure.SimpleExternalComment.Data;
using Goofy.Infrastructure.Core.Data.Extensions;

namespace Goofy.Application.SimpleExternalComment.DependencyInjection
{
    public class SimpleExternalCommentDependencyRegistrar : IDependencyRegistrar
    {
        public void ConfigureServices(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddUnitOfWork(typeof(ISimpleCommentUnitOfWork), typeof(SimpleCommentContext));
            services.AddScoped(typeof(ISimpleCommentServiceMapper<,>), typeof(SimpleCommentServiceMapper<,>));
            services.AddEntireCrudPermissions(typeof(Comment));
        }
    }
}
