using System.Threading.Tasks;

namespace Goofy.Security.Services
{
    public interface IUserSign
    {
        Task<bool> Login(string email, string password);
        Task LogOff();
    }
}