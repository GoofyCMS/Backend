
namespace Goofy.Core.Infrastructure
{
    public interface IRunAtStartup
    {
        void Run();
        int Order { get; }
    }
}
