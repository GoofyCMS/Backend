
namespace Goofy.Core.Entity.Base
{
    public class GoofyEntityBase
    {
        public GoofyEntityBase()
        {
            GlobalId = GoofyIdStaticProvider.GetNewId();
        }

        public long GlobalId
        {
            get;
            private set;
        }
    }
}