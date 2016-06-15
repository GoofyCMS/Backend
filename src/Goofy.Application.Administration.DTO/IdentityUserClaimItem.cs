
using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Administration.DTO
{
    public class IdentityUserClaimItem
    {
        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
