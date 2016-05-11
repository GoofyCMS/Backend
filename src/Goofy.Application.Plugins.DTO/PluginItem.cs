using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Plugins.DTO
{
    public class PluginItem
    {
        [Key]
        public int PluginId { get; set; }
        public bool Installed { get; set; }
    }
}
