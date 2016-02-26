using Goofy.Core.Infrastructure;

namespace Goofy.WebFramework.Components
{
    public class GoofyComponentsRunAtStartup : IRunAtStartup
    {
        public int Order
        {
            get
            {
                return -100;
            }
        }

        public void Run()
        {
            
        }
    }
}
