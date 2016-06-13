
namespace Goofy.Application.Core.Abstractions
{
    /*
       Services which implement this interface will be removed from
       will be available only during application load time. They will
       be removed from IServiceCollection when IEngine.Start() method ends.
   */
    public interface IDesignTimeService
    {
    }
}
