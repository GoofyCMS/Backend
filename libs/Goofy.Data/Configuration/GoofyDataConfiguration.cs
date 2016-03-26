
namespace Goofy.Data
{
    public class GoofyDataConfiguration
    {
        public string DataProviderName { get; set; } = "sqlite";

        public ConnectionConfiguration DefaultConnection { get; set; } = new ConnectionConfiguration();
    }
}
