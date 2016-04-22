
namespace Goofy.Core.Entity.Base
{
    public abstract class GoofyEntityBase
    {
        public GoofyEntityBase()
        {
            GlobalId = GoofyIdStaticProvider.GetNewId();
        }

        public long GlobalId
        {
            get;
            set;
        }
    }
}