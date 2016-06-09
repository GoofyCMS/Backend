using System.Threading.Tasks;

namespace Goofy.Security.Services
{
    public interface IUserRegister
    {
        Task<bool> Register(string email, string password);
    }
}