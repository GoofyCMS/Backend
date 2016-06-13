
namespace Goofy.Application.Core.Abstractions
{
    public interface IRunAtStartup : ISortableTask
    {
        void Run();
    }
}
