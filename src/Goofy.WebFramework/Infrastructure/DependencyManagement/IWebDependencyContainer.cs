using Microsoft.Extensions.Configuration;
using Goofy.Core.Infrastructure;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Components.Configuration;

namespace Goofy.WebFramework.Infrastructure
{
    public interface IWebDependencyContainer : IDependencyContainer
    {
        IServiceCollection ServiceCollection { get; }

        void RegisterConfigurations<TSetup>(IConfiguration configuration) where TSetup : ComponentConfig;

        void AddDbContextObject<T>() where T : DbContext;
    }
}
