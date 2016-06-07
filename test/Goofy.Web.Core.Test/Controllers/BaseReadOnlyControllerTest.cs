using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Goofy.Domain.Core.Entity;
using Goofy.Presentation.Core.Controllers;
using Goofy.Application.Core.Service;
using Goofy.Presentation.Core.Providers;
using Goofy.Domain.Core.Service.Data;
using System.Threading.Tasks;
using Goofy.Infrastructure.Core.Adapter;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Builder;
using System.Net.Http;
using Microsoft.AspNet.TestHost;
using Microsoft.AspNet.Mvc;
using Xunit;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Web.Core.Test.Controllers
{
    public class MockEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MockEntityViewModel { }
    public class MockTypeAdapter : AutomapperTypeAdapter
    {
        public MockTypeAdapter()
            : base(
                    new MapperConfiguration(conf =>
                                            {
                                                conf.CreateMap<MockEntity, MockEntityViewModel>();
                                                conf.CreateMap<MockEntityViewModel, MockEntity>();
                                            }
                    )
              )
        {

        }
    }
    public class MockServiceMapper : ServiceMapper<MockEntity, MockEntityViewModel>
    {
        public MockServiceMapper(MockUnitOfWork unitOfWork, MockTypeAdapter typeAdapter, MockRepository repo = null)
            : base(unitOfWork, typeAdapter, repo)
        { }
    }
    public class FakeContext { }
    public class MockRepository : IRepository<MockEntity>
    {
        IEnumerable<MockEntity> entities = new[] { new MockEntity { Id = 0, Name = "entity1" }, new MockEntity { Id = 1, Name = "entity2" }, new MockEntity { Id = 2, Name = "entity3" } };
        public MockEntity Add(MockEntity item)
        {
            throw new NotImplementedException();
        }

        public MockEntity Create()
        {
            throw new NotImplementedException();
        }

        public MockEntity Find(params object[] entityKeyValues)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MockEntity> GetAll(string sql, params object[] parameters)
        {
            return entities;
        }

        public IEnumerable<MockEntity> GetAll(Expression<Func<MockEntity, bool>> filter = null, bool noTracking = false, params Expression<Func<MockEntity, object>>[] joinedEntities)
        {
            return entities;
        }

        public MockEntity Modify(MockEntity item)
        {
            throw new NotImplementedException();
        }

        public MockEntity Remove(MockEntity item)
        {
            throw new NotImplementedException();
        }
    }
    public class MockUnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public IRepository<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            if (typeof(TEntity) == typeof(MockEntity))
            {
                return (IRepository<TEntity>)new MockRepository();
            }
            throw new NotImplementedException();
        }

        public IEnumerable<TResult> SqlQuery<TResult>(string query, params object[] parameters) where TResult : class
        {
            throw new NotImplementedException();
        }
    }
    public class MockContextProvider : BaseContextProvider<FakeContext>
    {
        public MockContextProvider(MockUnitOfWork unitOfWork, ITypeAdapterFactory factory = null) : base(null, unitOfWork, factory) { }
    }

    public class MockServerStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MockServiceMapper>();
            services.AddSingleton<MockContextProvider>();
            services.AddSingleton<MockUnitOfWork>();
            services.AddSingleton<MockTypeAdapter>();
            services.AddMvc().AddControllersAsServices(new[] { typeof(MockBaseReadOnlyController) });
        }

        public void Configure(IApplicationBuilder builder)
        {
            builder.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    [Route("test")]
    public class MockBaseReadOnlyController : BaseReadOnlyController<MockEntity, MockEntityViewModel, int>
    {
        public MockBaseReadOnlyController(MockServiceMapper serviceMapper, MockContextProvider mockContextProvider)
            : base(serviceMapper, mockContextProvider)
        {
        }
    }


    public class BaseReadOnlyControllerTest
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;

        public BaseReadOnlyControllerTest()
        {
            _server = new TestServer(TestServer.CreateBuilder()
                                               .UseStartup<MockServerStartup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public void EnsureRequestIsSent()
        {
            var result = _client.GetAsync("/test/MockEntity").Result;
            result.EnsureSuccessStatusCode();
            var responseString = result.Content.ReadAsStringAsync().Result;
            Assert.Equal("Hello World", responseString);
        }

    }
}
