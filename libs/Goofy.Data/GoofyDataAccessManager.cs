using Goofy.Core.Infrastructure;
using Goofy.Data.DataProvider;
using Goofy.Configuration.Extensions;
using System;
using Goofy.Data.DataProvider.Services;

namespace Goofy.Data
{
    public class GoofyDataAccessManager
    {
        private const string SqlDataProviderName = "sql";
        private const string SqliteDataProviderName = "sqlite";

        private GoofyDataConfiguration _goofyDataConfiguration;
        private Type _dataProviderType;
        private Type _dataProviderConfiguratorType;

        public GoofyDataAccessManager()
        {
        }

        public GoofyDataConfiguration GoofyDataConfiguration
        {
            get
            {
                if (_goofyDataConfiguration == null)
                {
                    _goofyDataConfiguration = new GoofyDataConfiguration();
                }
                return _goofyDataConfiguration;
            }
        }

        public Type DataProviderConfiguratorType
        {
            get
            {
                if (_dataProviderConfiguratorType != null)
                {
                    return _dataProviderConfiguratorType;
                }
                var dataProviderName = GoofyDataConfiguration.DataProviderName;
                if (dataProviderName == SqlDataProviderName)
                {
                    _dataProviderConfiguratorType = typeof(SqlDataProviderConfigurator);
                }
                else if (dataProviderName == SqliteDataProviderName)
                {
                    _dataProviderConfiguratorType = typeof(SqliteDataProviderConfigurator);
                }
                else
                {
                    throw new ArgumentException(string.Format("Non DataProvider found for {0}.", GoofyDataConfiguration.DataProviderName));
                }
                return _dataProviderConfiguratorType;
            }
        }

        public Type DataProviderType
        {
            get
            {
                if (_dataProviderType != null)
                {
                    return _dataProviderType;
                }
                var dataProviderName = GoofyDataConfiguration.DataProviderName;
                if (dataProviderName == SqlDataProviderName)
                {
                    _dataProviderType = typeof(SqlDataProviderWrapper);
                }
                else if (dataProviderName == SqliteDataProviderName)
                {
                    _dataProviderType = typeof(SqliteDataProviderWrapper);
                }
                else
                {
                    throw new ArgumentException(string.Format("Non DataProvider found for {0}.", GoofyDataConfiguration.DataProviderName));
                }
                return _dataProviderType;
            }
        }
    }
}
