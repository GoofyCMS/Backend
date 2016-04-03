using Goofy.Core.Entity.Base;

namespace Goofy.Core.Components
{
    public class Component : GoofyEntityBase
    {
        public int ComponentId { get; set; }
        public string FullName { get; set; }
        public string Version { get; set; }
        public bool Installed { get; set; }
        public bool IsSystemComponent { get; set; }
    }
}
