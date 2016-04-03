
namespace Goofy.Core.Entity.Base
{
    public static class GoofyIdStaticProvider
    {
        static long _currentId = long.MinValue;

        public static long GetNewId()
        {
            return _currentId++;
        }
    }
}
