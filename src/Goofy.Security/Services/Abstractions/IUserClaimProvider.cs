using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Goofy.Security.Services.Abstractions
{
    public interface IUserClaimProvider
    {
        Task<IEnumerable<Claim>> GetUserClaims(string userName, string password);
    }
}
