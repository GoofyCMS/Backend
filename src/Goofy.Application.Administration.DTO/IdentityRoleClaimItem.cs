
using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Administration.DTO
{
    public class IdentityRoleClaimItem
    {
        [Key]
        public string Id { get; set; }

        public string RoleId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
