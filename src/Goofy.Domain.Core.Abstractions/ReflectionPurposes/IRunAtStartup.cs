
namespace Goofy.Domain.Core.Abstractions
{
    public interface IRunAtStartup : ISortableTask
    {
        void Run();
    }
}
