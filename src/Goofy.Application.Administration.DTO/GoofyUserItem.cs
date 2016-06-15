
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Administration.DTO
{
    public class GoofyUserItem
    {
        [Key]
        public string Id { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        public virtual ICollection<IdentityUserRoleItem> Roles { get; set; }

        public virtual ICollection<IdentityUserClaimItem> Claims { get; set; }
    }
}
