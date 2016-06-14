using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Administration.DTO
{
    public class GoofyRoleItem
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<IdentityRoleClaimItem> Claims { get; set; }
    }
}
