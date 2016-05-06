namespace Goofy.Domain.Core.Entity
{
    public abstract class SoftDeleteEntity : IdentityEntity
    {
        public bool IsDeleted { get; set; }
    }
}