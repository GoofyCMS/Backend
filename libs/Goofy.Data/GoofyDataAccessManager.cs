using Goofy.Core.Infrastructure;
using Goofy.Configuration.Extensions;

namespace Goofy.Data
{
    public class GoofyDataAccessManager
    {
        private static GoofyDataConfiguration _goofyDataConfiguration;
        public static GoofyDataConfiguration GoofyDataConfiguration
        {
            get
            {
                if (_goofyDataConfiguration == null)
                {
                    string jsonFilePath = string.Format("{0}\\{1}", GoofyDomainResourceLocator.GetBinDirectoryPath(), ConfigurationSource);
                    _goofyDataConfiguration = ConfigurationExtensions.GetConfiguration<GoofyDataConfiguration>(jsonFilePath, ConfigurationSection);
                }
                return _goofyDataConfiguration;
            }
        }

        public static string ConfigurationSource
        {
            get
            {
                return @"Goofy.Data\config.json";
            }
        }

        public static string ConfigurationSection
        {
            get
            {
                return "GoofyDataSection";
            }
        }
    }
}
