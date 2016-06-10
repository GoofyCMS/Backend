using Goofy.Domain.Identity.Entity;

namespace Goofy.Domain.Administration.Entity
{
    /// <summary>
    /// Esta clase rol es para los roles de Identity
    /// </summary>
    public class GoofyRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
