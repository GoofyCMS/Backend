
namespace Goofy.Core.Components.Configuration
{
    public interface IConfigurator<T> where T : ComponentConfig
    {
        void Configure(T config);
    }
}
