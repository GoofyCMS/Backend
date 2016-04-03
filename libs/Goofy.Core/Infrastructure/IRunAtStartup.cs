
namespace Goofy.Core.Infrastructure
{
    public interface IRunAtStartup : ISortableTask
    {
        void Run();
    }
}
