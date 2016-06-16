using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Administration.DTO
{
    public class PermissionItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
