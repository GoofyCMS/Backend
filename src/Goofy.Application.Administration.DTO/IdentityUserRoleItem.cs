using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goofy.Application.Administration.DTO
{
    public class IdentityUserRoleItem
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("GoofyUser")]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string RoleId { get; set; }

        public virtual GoofyUserItem GoofyUser { get; set; }
    }
}
