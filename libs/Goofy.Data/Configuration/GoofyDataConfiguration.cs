using System;
using Goofy.Data.DataProvider;

namespace Goofy.Data
{
    public class GoofyDataConfiguration
    {
        private const string SqlDataProviderName = "sql";
        private const string SqliteDataProviderName = "sqlite";

        private IEntityFrameworkDataProviderWrapper _provider;

        public string DataProviderName { get; set; }

        public IEntityFrameworkDataProviderWrapper Provider
        {
            get
            {
                if (_provider != null)
                    return _provider;
                if (DataProviderName == SqlDataProviderName)
                {
                    _provider = new SqlDataProviderWrapper();
                }
                else if (DataProviderName == SqliteDataProviderName)
                {
                    _provider = new SqliteDataProvider();
                }
                else
                {
                    throw new ArgumentException(string.Format("Non DataProvider found for {0}.", DataProviderName));
                }
                return _provider;
            }
        }

        public ConnectionConfiguration DefaultConnection { get; set; }


    }
}
