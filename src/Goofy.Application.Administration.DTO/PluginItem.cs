using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Administration.DTO
{
    public class PluginItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
