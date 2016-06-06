using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.PluggableCore.DTO
{
    public class PluginItem
    {
        [Key]
        public int PluginId { get; set; }
        public string Name { get; set; }
        public bool Installed { get; set; }
    }
}
