
using System.ComponentModel.DataAnnotations;

namespace Goofy.Application.Plugins.DTO
{
    public class PluginItem
    {
        [Key]
        public int ComponentId { get; set; }

        public string FullName { get; set; }
        public string Version { get; set; }
        public bool Installed { get; set; }
        public bool IsSystemComponent { get; set; }
    }
}
