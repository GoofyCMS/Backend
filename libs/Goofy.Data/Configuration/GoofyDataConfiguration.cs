using System;
using Goofy.Data.DataProvider;

namespace Goofy.Data
{
    public class GoofyDataConfiguration
    {
        public string DataProviderName { get; set; }

        public ConnectionConfiguration DefaultConnection { get; set; }
    }
}
