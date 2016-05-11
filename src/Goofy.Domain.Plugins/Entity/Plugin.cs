
using Goofy.Domain.Core.Entity;

namespace Goofy.Domain.Plugins.Entity
{
    public class Plugin : BaseEntity
    {
        public int PluginId { get; set; }
        public bool Installed { get; set; }
    }
}
