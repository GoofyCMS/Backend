using Goofy.Domain.Core.Entity;

namespace Goofy.Domain.Administration.Entity
{
    public class Plugin : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
